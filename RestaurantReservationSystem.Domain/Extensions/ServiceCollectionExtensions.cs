using Microsoft.Extensions.DependencyInjection;
using RestaurantReservationSystem.Domain.Interfaces.Services;
using RestaurantReservationSystem.Domain.Services;
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDomainServices(this IServiceCollection services)
    {
        services.AddScoped<IRestaurantService, RestaurantService>();
        services.AddScoped<IEmployeeService, EmployeeService>();
        services.AddScoped<ITableService, TableService>();
        services.AddScoped<IMenuItemService, MenuItemService>();
        services.AddScoped<IReservationService, ReservationService>();
        services.AddScoped<IOrderService, OrderService>();

        return services;
    }
}