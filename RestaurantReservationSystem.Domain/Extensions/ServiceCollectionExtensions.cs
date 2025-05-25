using Microsoft.Extensions.DependencyInjection;
using RestaurantReservationSystem.API.Interfaces;
using RestaurantReservationSystem.API.Services;
using RestaurantReservationSystem.API.Services.Interfaces;
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDomainServices(this IServiceCollection services)
    {
        services.AddScoped<IRestaurantService, RestaurantService>();
        services.AddScoped<IEmployeeService, EmployeeService>();
        services.AddScoped<ITableService, TableService>();

        return services;
    }
}