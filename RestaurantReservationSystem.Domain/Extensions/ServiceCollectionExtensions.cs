using Microsoft.Extensions.DependencyInjection;
using RestaurantReservationSystem.API.Services;
using RestaurantReservationSystem.Domain.Interfaces.Services;
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