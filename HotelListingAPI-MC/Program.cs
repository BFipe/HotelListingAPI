using HotelListingAPI_DATA;
using HotelListingAPI_MC.Configurations;
using HotelListingAPI_MC.Contracts;
using HotelListingAPI_MC.Repositories;
using Microsoft.EntityFrameworkCore;
using Serilog;

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

            var connectionString = builder.Configuration.GetConnectionString("HotelListingDbConnectionString");
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
            });

            builder.Services.AddAutoMapper(typeof(MapperConfig));

            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            builder.Services.AddScoped<ICountryRepository, CountryRepository>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            //Added serilog logging with settings in appsettings.json
            app.UseSerilogRequestLogging();

            app.UseHttpsRedirection();

            app.UseAuthorization();

            //Applying cors
            app.UseCors("AllowAll");

            app.MapControllers();

            app.Run();
        }
    }
}