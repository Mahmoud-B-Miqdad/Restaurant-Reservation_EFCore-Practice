using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using RestaurantReservation.Db;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RestaurantReservation.Services;
using RestaurantReservation.Db.Repositories;

var host = Host.CreateDefaultBuilder()
    .ConfigureLogging(logging =>
     {
         logging.ClearProviders();
     })
    .ConfigureServices((context, services) =>
    {
        services.AddDbContext<RestaurantReservationDbContext>(options =>
            options.UseSqlServer(context.Configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<RestaurantOperations>();
        services.AddScoped<RestaurantService>();

        services.AddScoped<CustomerOperations>();
        services.AddScoped<CustomerService>();

        services.AddScoped<EmployeeOperations>();
        services.AddScoped<EmployeeService>();

        services.AddScoped<ReservationOperations>();
        services.AddScoped<ReservationService>();

        services.AddScoped<TableOperations>();
        services.AddScoped<TableService>();

        services.AddScoped<MenuItemOperations>();
        services.AddScoped<MenuItemService>();

        services.AddScoped<OrderOperations>();
        services.AddScoped<OrderService>();

        services.AddScoped<OrderItemOperations>();
        services.AddScoped<OrderItemService>();

        services.AddScoped<EmployeeRepository>();
        services.AddScoped<ReservationRepository>();
        services.AddScoped<OrderRepository>();
        services.AddScoped<MenuItemRepository>();

        services.AddScoped<ReservationReportRepository>();
        services.AddScoped<EmployeeReportRepository>();
        services.AddScoped<RevenueReportRepository>();

        services.AddScoped<AppUtilities>();
    })
    .Build();

var app = host.Services.GetRequiredService<AppUtilities>();
await app.RunAsync();