using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Db.Seeders;

public class ReservationSeeder
{
    public async Task<List<Reservation>> SeedAsync(RestaurantReservationDbContext context, List<Customer> customers, List<Restaurant> restaurants, List<Table> tables)
    {
        var reservations = new List<Reservation>
        {
            new() { CustomerId = customers[0].CustomerId, RestaurantId = restaurants[0].RestaurantId, TableId = tables[0].TableId, ReservationDate = DateTime.Today.AddDays(1), PartySize = 2 },
            new() { CustomerId = customers[1].CustomerId, RestaurantId = restaurants[1].RestaurantId, TableId = tables[2].TableId, ReservationDate = DateTime.Today.AddDays(2), PartySize = 3 },
            new() { CustomerId = customers[2].CustomerId, RestaurantId = restaurants[2].RestaurantId, TableId = tables[4].TableId, ReservationDate = DateTime.Today.AddDays(3), PartySize = 4 },
            new() { CustomerId = customers[3].CustomerId, RestaurantId = restaurants[3].RestaurantId, TableId = tables[6].TableId, ReservationDate = DateTime.Today.AddDays(4), PartySize = 2 },
            new() { CustomerId = customers[4].CustomerId, RestaurantId = restaurants[4].RestaurantId, TableId = tables[8].TableId, ReservationDate = DateTime.Today.AddDays(5), PartySize = 5 }
        };

        await context.Reservations.AddRangeAsync(reservations);
        await context.SaveChangesAsync();

        return reservations;
    }
}