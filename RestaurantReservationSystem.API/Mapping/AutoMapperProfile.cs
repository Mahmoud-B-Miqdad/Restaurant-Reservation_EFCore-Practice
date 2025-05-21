using AutoMapper;
using RestaurantReservation.Db.Entities;
using RestaurantReservationSystem.API.DTOs;

namespace RestaurantReservationSystem.API.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Restaurant, RestaurantResponse>();
            CreateMap<RestaurantRequest, Restaurant>();
        }
    }
}