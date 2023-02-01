using AutoMapper;
using HotelListingAPI_MC.Data.Entities.CountryEntities;
using HotelListingAPI_MC.Data.Entities.HotelEntities;
using HotelListingAPI_MC.Data.Entities.UserEntities;
using HotelListingAPI_MC.Models.Country;
using HotelListingAPI_MC.Models.Hotel;
using HotelListingAPI_MC.Models.User;

namespace HotelListingAPI_MC.Configurations
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
