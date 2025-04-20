using RestaurantReservation.Db;
using RestaurantReservation.Db.Models;
using RestaurantReservation.Db.Seeders;

public class AppUtilities
{
    private readonly RestaurantReservationDbContext _context;
    private readonly RestaurantService _restaurantService;
    private readonly CustomerService _customerOperations;
    private readonly EmployeeService _employeeOperations;
    public AppUtilities(RestaurantReservationDbContext context, RestaurantService restaurantService,
        CustomerService customerOperations, EmployeeService employeeOperations)
    {
        _context = context;
        _restaurantService = restaurantService;
        _customerOperations = customerOperations;
        _employeeOperations = employeeOperations;
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
        //    await _customerOperations.ExecuteExamplesAsync();
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
            await _employeeOperations.ExecuteExamplesAsync();
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
