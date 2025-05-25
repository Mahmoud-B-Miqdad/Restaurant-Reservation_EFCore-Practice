using RestaurantReservationSystem.API.Services;
using RestaurantReservationSystem.API.Services.Interfaces;
using JwtAuthMinimalApi.Configurations;
using JwtAuthMinimalApi.Services;
using RestaurantReservationSystem.API.Interfaces;

namespace RestaurantReservationSystem.API.Extensions
{
    /// <summary>
    /// Provides extension methods for registering application services and configurations.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Registers application-specific services, AutoMapper, JWT configuration, and repositories.
        /// </summary>
        /// <param name="services">The service collection to which services will be added.</param>
        /// <param name="configuration">The application configuration containing necessary settings.</param>
        /// <returns>The updated service collection with registered services.</returns>
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtSettingsSection = configuration.GetSection("JwtSettings");
            services.Configure<JwtSettings>(jwtSettingsSection);

            var jwtSettings = jwtSettingsSection.Get<JwtSettings>();

            services.AddAutoMapper(typeof(RestaurantReservationSystem.API.Mapping.AutoMapperProfile).Assembly);

            services.AddScoped<IAuthorizationService, AuthorizationService>();
            services.AddSingleton<IJwtTokenGenerator>(new JwtTokenGenerator(jwtSettings));

            services.AddRepositories(configuration.GetConnectionString("DefaultConnection"));

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
}