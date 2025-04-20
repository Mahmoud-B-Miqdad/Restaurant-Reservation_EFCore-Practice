using RestaurantReservation.Db;
using RestaurantReservation.Db.Seeders;

public class AppUtilities
{
    private readonly RestaurantReservationDbContext _context;
    private readonly RestaurantService _restaurantService;
    public AppUtilities(RestaurantReservationDbContext context, RestaurantService restaurantService)
    {
        _context = context;
        _restaurantService = restaurantService;
    }

    public async Task RunAsync()
    {
        await RestaurantReservationSeeder.SeedAsync(_context);

        try
        {
            await _restaurantService.ExecuteExamplesAsync();
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
