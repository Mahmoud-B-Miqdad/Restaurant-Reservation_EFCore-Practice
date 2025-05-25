using Microsoft.Extensions.DependencyInjection;
using RestaurantReservation.Db.Services.Interfaces;
using RestaurantReservationSystem.EntityServices.Services.Interfaces;
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDomainServices(this IServiceCollection services, string connectionString)
    {
        services.AddScoped<IRestaurantService, RestaurantService>();
        services.AddScoped<IEmployeeService, EmployeeService>();
        services.AddScoped<ITableService, TableService>();
        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<IReservationService, ReservationService>();
        services.AddScoped<IMenuItemService, MenuItemService>();
        services.AddScoped<ICustomerService, CustomerService>();
        services.AddScoped<IOrderItemService, OrderItemService>();

        return services;
    }
}