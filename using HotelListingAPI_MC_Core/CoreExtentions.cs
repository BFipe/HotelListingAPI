using HotelListingAPI_MC_Core.Configurations;
using HotelListingAPI_MC_Core.Contracts;
using HotelListingAPI_MC_Core.Middlewares;
using HotelListingAPI_MC_Core.Repositories;
using HotelListingAPI_MC_Data;
using Microsoft.Extensions.DependencyInjection;

namespace HotelListingAPI_MC_Core
{
    public static class CoreExtentions
    {
        public static IServiceCollection AddCoreExtentions(this IServiceCollection services, string connectionString)
        {
            services.AddDatabase(connectionString);

            services.AddAutoMapper(typeof(MapperConfig));
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<ICountryRepository, CountryRepository>();
            services.AddScoped<IHotelRepository, HotelRepository>();
            services.AddScoped<IAuthManager, AuthManager>();
            services.AddTransient<ExceptionMiddleware>();

            return services;
        }
    }
}
