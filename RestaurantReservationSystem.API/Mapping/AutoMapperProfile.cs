using AutoMapper;
using RestaurantReservation.Db.Entities;
using RestaurantReservationSystem.API.DTOs.Requests;
using RestaurantReservationSystem.API.DTOs.Responses;

namespace RestaurantReservationSystem.API.Mapping
{
    /// <summary>
    /// Defines mapping configuration between domain entities and DTOs using AutoMapper.
    /// </summary>
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Restaurant, RestaurantResponse>();
            CreateMap<RestaurantRequest, Restaurant>();

            CreateMap<Employee, EmployeeResponse>();
            CreateMap<EmployeeRequest, Employee>();

            CreateMap<Table, TableResponse>();
            CreateMap<TableRequest, Table>();

            CreateMap<MenuItem, MenuItemResponse>();
            CreateMap<MenuItemRequest, MenuItem>();

            CreateMap<Reservation, ReservationResponse>();
            CreateMap<ReservationRequest, Reservation>();

            CreateMap<Order, OrderResponse>();
            CreateMap<OrderRequest, Order>();

            CreateMap<OrderItem, OrderItemResponse>();
            CreateMap<OrderItemRequest, OrderItem>();
        }
    }
}