using RestaurantReservation.Db;
using RestaurantReservation.Db.Models;
using RestaurantReservation.Db.Seeders;

public class AppUtilities
{
    private readonly RestaurantReservationDbContext _context;
    private readonly RestaurantService _restaurantService;
    private readonly CustomerService _customerService;
    private readonly EmployeeService _employeeService;
    private readonly ReservationService _reservationServices;
    private readonly TableService _tableServices;
    private readonly MenuItemService _menuItemServices;
    private readonly OrderService _orderServices;
    public AppUtilities(RestaurantReservationDbContext context, RestaurantService restaurantService,
        CustomerService customerService, EmployeeService employeeService,
        ReservationService reservationService, TableService tableServices,
        MenuItemService menuItemServices, OrderService orderServices)
    {
        _context = context;
        _restaurantService = restaurantService;
        _customerService = customerService;
        _employeeService = employeeService;
        _reservationServices = reservationService;
        _tableServices = tableServices;
        _menuItemServices = menuItemServices;
        _orderServices = orderServices;
    }

    public async Task RunAsync()
    {
        //await RestaurantReservationSeeder.SeedAsync(_context);

        //try
        //{
        //    await _restaurantService.ExecuteExamplesAsync();
        //}
        //catch (InvalidOperationException ex)
        //{
        //    Console.WriteLine($"Operation failed: {ex.Message}");
        //}
        //catch (Exception ex)
        //{
        //    Console.WriteLine($"Unexpected error: {ex.Message}");
        //}

        //try
        //{
        //    await _customerService.ExecuteExamplesAsync();
        //}
        //catch (InvalidOperationException ex)
        //{
        //    Console.WriteLine($"Operation failed: {ex.Message}");
        //}
        //catch (Exception ex)
        //{
        //    Console.WriteLine($"Unexpected error: {ex.Message}");
        //}


        //try
        //{
        //    await _employeeService.ExecuteExamplesAsync();
        //}
        //catch (InvalidOperationException ex)
        //{
        //    Console.WriteLine($"Operation failed: {ex.Message}");
        //}
        //catch (Exception ex)
        //{
        //    Console.WriteLine($"Unexpected error: {ex.Message}");
        //}

        //try
        //{
        //    await _reservationServices.ExecuteExamplesAsync();
        //}
        //catch (InvalidOperationException ex)
        //{
        //    Console.WriteLine($"Operation failed: {ex.Message}");
        //}
        //catch (Exception ex)
        //{
        //    Console.WriteLine($"Unexpected error: {ex.Message}");
        //}

        //try
        //{
        //    await _tableServices.ExecuteExamplesAsync();
        //}
        //catch (InvalidOperationException ex)
        //{
        //    Console.WriteLine($"Operation failed: {ex.Message}");
        //}
        //catch (Exception ex)
        //{
        //    Console.WriteLine($"Unexpected error: {ex.Message}");
        //}

        //try
        //{
        //    await _menuItemServices.ExecuteExamplesAsync();
        //}
        //catch (InvalidOperationException ex)
        //{
        //    Console.WriteLine($"Operation failed: {ex.Message}");
        //}
        //catch (Exception ex)
        //{
        //    Console.WriteLine($"Unexpected error: {ex.Message}");
        //}

        try
        {
            await _orderServices.ExecuteExamplesAsync();
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine($"Operation failed: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Unexpected error: {ex.Message}");
        }
    }
}
