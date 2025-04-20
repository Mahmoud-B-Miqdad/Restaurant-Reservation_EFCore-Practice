using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using RestaurantReservation.Db;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

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

        services.AddScoped<AppUtilities>();
    })
    .Build();

var app = host.Services.GetRequiredService<AppUtilities>();
await app.RunAsync();