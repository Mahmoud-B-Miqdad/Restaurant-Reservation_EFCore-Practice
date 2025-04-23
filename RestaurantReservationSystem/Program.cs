using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using RestaurantReservation.Db;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RestaurantReservation.Db.Repositories;
using RestaurantReservation.Db.Seeders;
using RestaurantReservation.Db.Repositories.ReportRepositories;

var host = Host.CreateDefaultBuilder()
    .ConfigureLogging(logging =>
     {
         logging.ClearProviders();
     })
    .ConfigureServices((context, services) =>
    {
        services.AddDbContext<RestaurantReservationDbContext>(options =>
            options.UseSqlServer(context.Configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<RestaurantReservationSeeder>();

        services.AddScoped<RestaurantRepository>();
        services.AddScoped<RestaurantService>();

        services.AddScoped<CustomerRepository>();
        services.AddScoped<CustomerService>();

        services.AddScoped<EmployeeRepository>();
        services.AddScoped<EmployeeService>();

        services.AddScoped<ReservationRepository>();
        services.AddScoped<ReservationService>();

        services.AddScoped<TableRepository>();
        services.AddScoped<TableService>();

        services.AddScoped<MenuItemRepository>();
        services.AddScoped<MenuItemService>();

        services.AddScoped<OrderRepository>();
        services.AddScoped<OrderService>();

        services.AddScoped<OrderItemRepository>();
        services.AddScoped<OrderItemService>();

        services.AddScoped<EmployeeRepository>();
        services.AddScoped<ReservationRepository>();
        services.AddScoped<OrderRepository>();
        services.AddScoped<MenuItemRepository>();

        services.AddScoped<ReservationReportRepository>();
        services.AddScoped<EmployeeReportRepository>();
        services.AddScoped<RevenueReportRepository>();
        services.AddScoped<CustomerReportRepository>();

        services.AddScoped<AppUtilities>();
    })
    .Build();

var app = host.Services.GetRequiredService<AppUtilities>();
await app.RunAsync();