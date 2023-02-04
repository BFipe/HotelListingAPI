using HotelListingAPI_MC_Data.Entities.UserEntities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace HotelListingAPI_MC_Data
{
    public static class DatabaseExtentions
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<HotelListingDbContext>(options =>
            {
                options.UseSqlServer(connectionString, q => q.MigrationsAssembly("HotelListingAPI_MC_Data"));
            });

            services
                .AddIdentityCore<APIUser>(options => 
                {
                    options.Password.RequireNonAlphanumeric = false;
                })
                .AddRoles<IdentityRole>()
                .AddTokenProvider<DataProtectorTokenProvider<APIUser>>("HotelListingAPI-MC")
                .AddEntityFrameworkStores<HotelListingDbContext>()
                .AddDefaultTokenProviders();

            return services;
        }
    }
}
