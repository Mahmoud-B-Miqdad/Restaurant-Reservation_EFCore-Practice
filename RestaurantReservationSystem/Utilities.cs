using RestaurantReservation.Db;
using RestaurantReservation.Db.Seeders;

public class AppUtilities
{
    private readonly RestaurantReservationDbContext _context;

    public AppUtilities(RestaurantReservationDbContext context)
    {
        _context = context;
    }

    public async Task RunAsync()
    {
        await RestaurantReservationSeeder.SeedAsync(_context);
    }
}
