using HotelListingAPI_MC.Data;
using HotelListingAPI_MC.Data.Entities.UserEntities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelListingAPI_DATA
{
    public static class DatabaseExtentions
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<HotelListingDbContext>(options =>
            {
                options.UseSqlServer(connectionString, q => q.MigrationsAssembly("HotelListingAPI-MC"));
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
