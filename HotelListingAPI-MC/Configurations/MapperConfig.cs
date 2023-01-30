using AutoMapper;
using HotelListingAPI_DATA.Entities;
using HotelListingAPI_MC.Models.Country;
using HotelListingAPI_MC.Models.Hotel;

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
        }

    }
}
