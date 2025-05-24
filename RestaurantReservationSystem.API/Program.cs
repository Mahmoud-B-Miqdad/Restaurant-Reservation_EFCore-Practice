using RestaurantReservationSystem.API.Interfaces;
using RestaurantReservationSystem.API.Services;
using RestaurantReservationSystem.API.Services.Interfaces;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutoMapper(typeof(RestaurantReservationSystem.API.Mapping.AutoMapperProfile).Assembly);

builder.Services.AddRepositories(builder.Configuration.GetConnectionString("DefaultConnection"));
builder.Services.AddScoped<IRestaurantService, RestaurantService>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<ITableService, TableService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IReservationService, ReservationService>();
builder.Services.AddScoped<IMenuItemService, MenuItemService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IOrderItemService, OrderItemService>();

builder.Services.AddControllers()
    .AddNewtonsoftJson();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);
});

var app = builder.Build();

app.UseSwagger(); 
app.UseSwaggerUI();

app.MapControllers();

app.Run();
