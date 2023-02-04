using HotelListingAPI_DATA;
using HotelListingAPI_MC.Configurations;
using HotelListingAPI_MC.Contracts;
using HotelListingAPI_MC.Middlewares;
using HotelListingAPI_MC.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Text;

namespace HotelListingAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var connectionString = builder.Configuration["HotelListingDbConnectionString"];
            builder.Services.AddDatabase(connectionString);

            //Configuring cors
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", allow =>
                {
                    allow.AllowAnyHeader();
                    allow.AllowAnyMethod();
                    allow.AllowAnyOrigin();
                });
            });

            builder.Host.UseSerilog((ctx, lc) =>
            {
                lc.WriteTo.Console().ReadFrom.Configuration(ctx.Configuration);
                lc.WriteTo.Seq("http://localhost:5341").ReadFrom.Configuration(ctx.Configuration);
            });

            builder.Services.AddAutoMapper(typeof(MapperConfig));

            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            builder.Services.AddScoped<ICountryRepository, CountryRepository>();
            builder.Services.AddScoped<IHotelRepository, HotelRepository>();
            builder.Services.AddScoped<IAuthManager, AuthManager>();
            builder.Services.AddTransient<ExceptionMiddleware>();

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer= true,
                    ValidateLifetime= true,
                    ValidateAudience = true,
                    ClockSkew = TimeSpan.Zero,
                    ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
                    ValidAudience = builder.Configuration["JwtSettings:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtKey"]))
                    
                };
            });

            builder.Services.AddApiVersioning(options =>
            {
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
                options.ReportApiVersions = true;
                options.ApiVersionReader = ApiVersionReader.Combine(
                    new QueryStringApiVersionReader("api-version"),
                    new HeaderApiVersionReader("X-Version"),
                    new MediaTypeApiVersionReader("ver")
                    );
            });

            builder.Services.AddVersionedApiExplorer(
                options =>
                {
                    options.GroupNameFormat = "'v'VVV";
                    options.SubstituteApiVersionInUrl = true;
                });

            builder.Services.AddResponseCaching(options =>
            {
                options.MaximumBodySize = 1024;
                options.UseCaseSensitivePaths = true;
            });


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseMiddleware<ExceptionMiddleware>();

            //Added serilog logging with settings in appsettings.json
            app.UseSerilogRequestLogging();

            app.UseHttpsRedirection();

            //Applying cors
            app.UseCors("AllowAll");

            app.UseResponseCaching();

            app.Use(async (context, next) => 
            {
                context.Response.GetTypedHeaders().CacheControl =
                new Microsoft.Net.Http.Headers.CacheControlHeaderValue()
                {
                    Public = true,
                    MaxAge = TimeSpan.FromSeconds(5),
                };

                context.Response.Headers[Microsoft.Net.Http.Headers.HeaderNames.Vary] =
                    new string[] { "Accept-Encoding" };
                
                await next();
            });


            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}