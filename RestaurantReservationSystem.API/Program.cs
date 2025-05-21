using RestaurantReservationSystem.API.Services;
using RestaurantReservationSystem.API.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutoMapper(typeof(RestaurantReservationSystem.API.Mapping.AutoMapperProfile).Assembly);

builder.Services.AddRepositories(builder.Configuration.GetConnectionString("DefaultConnection"));
builder.Services.AddScoped<IRestaurantService, RestaurantService>();

builder.Services.AddControllers()
    .AddNewtonsoftJson();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.MapControllers();

app.Run();
