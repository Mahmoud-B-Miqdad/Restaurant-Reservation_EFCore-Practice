using Microsoft.Extensions.DependencyInjection;
using RestaurantReservation.Db.Repositories;
using RestaurantReservation.Db.Repositories.Interfaces;
using RestaurantReservation.Db.Repositories.ReportRepositories;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<IRestaurantRepository, RestaurantRepository>();
        services.AddScoped<IEmployeeRepository, EmployeeRepository>();
        services.AddScoped<IMenuItemRepository, MenuItemRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IOrderItemRepository, OrderItemRepository>();
        services.AddScoped<IReservationRepository, ReservationRepository>();
        services.AddScoped<ITableRepository, TableRepository>();

        services.AddScoped<IReservationReportRepository, ReservationReportRepository>();
        services.AddScoped<IEmployeeReportRepository, EmployeeReportRepository>();
        services.AddScoped<IRevenueReportRepository, RevenueReportRepository>();
        services.AddScoped<ICustomerReportRepository, CustomerReportRepository>();
        return services;
    }
}
