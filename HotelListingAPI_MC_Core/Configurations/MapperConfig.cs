using AutoMapper;
using HotelListingAPI_MC_Data.Entities.CountryEntities;
using HotelListingAPI_MC_Data.Entities.HotelEntities;
using HotelListingAPI_MC_Data.Entities.UserEntities;
using HotelListingAPI_MC_Core.Models.Country;
using HotelListingAPI_MC_Core.Models.Hotel;
using HotelListingAPI_MC_Core.Models.User;

namespace HotelListingAPI_MC_Core.Configurations
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<CountryEntity, CreateCountryDto>().ReverseMap();
            CreateMap<CountryEntity, GetCountryDto>().ReverseMap();
            CreateMap<CountryEntity, UpdateCountryDto>().ReverseMap();
            CreateMap<CountryEntity, CountryDto>().ReverseMap();

            CreateMap<HotelEntity, CreateHotelDto>().ReverseMap();
            CreateMap<HotelEntity, GetHotelDto>().ReverseMap();
            CreateMap<HotelEntity, UpdateHotelDto>().ReverseMap();
            CreateMap<HotelEntity, HotelDto>().ReverseMap();

            CreateMap<APIUser, RegisterUserDto>().ReverseMap();
        }

    }
}
