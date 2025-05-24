using RestaurantReservationSystem.API.Services;
using RestaurantReservationSystem.API.Services.Interfaces;
using JwtAuthMinimalApi.Configurations;
using JwtAuthMinimalApi.Services;
using RestaurantReservationSystem.API.Interfaces;

namespace RestaurantReservationSystem.API.Extensions
{
    public static class ServiceCollectionExtensions
    {
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