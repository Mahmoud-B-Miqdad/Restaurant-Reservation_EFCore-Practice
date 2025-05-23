using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using RestaurantReservation.Db;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RestaurantReservation.Db.Services.Interfaces;
using RestaurantReservationSystem.EntityServices.Services.Interfaces;

var host = Host.CreateDefaultBuilder()
    .ConfigureLogging(logging =>
    {
        logging.ClearProviders();
    })
    .ConfigureServices((context, services) =>
    {
        services.AddDbContext<RestaurantReservationDbContext>(options =>
            options.UseSqlServer(context.Configuration.GetConnectionString("DefaultConnection")));

        services.AddRepositories(context.Configuration.GetConnectionString("DefaultConnection"));

        services.AddScoped<ICustomerService, CustomerService>();
        services.AddScoped<IRestaurantService, RestaurantService>();
        services.AddScoped<IEmployeeService, EmployeeService>();
        services.AddScoped<IReservationService, ReservationService>();
        services.AddScoped<ITableService, TableService>();
        services.AddScoped<IMenuItemService, MenuItemService>();
        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<IOrderItemService, OrderItemService>();

        services.AddScoped<AppUtilities>();
    })
    .Build();

var app = host.Services.GetRequiredService<AppUtilities>();
await app.RunAsync();