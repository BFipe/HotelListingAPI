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
                options.UseSqlServer(connectionString, q => q.MigrationsAssembly("HotelListingAPI-DATA"));
            });
           
            return services;
        }

    }
}
