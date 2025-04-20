using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Db.Seeders;

public class TableSeeder
{
    public async Task<List<Table>> SeedAsync(RestaurantReservationDbContext context, List<Restaurant> restaurants)
    {
        var tables = new List<Table>();

            tables.AddRange(new[]
            {
                new Table { RestaurantId = restaurants[0].RestaurantId, Capacity = 2 },
                new Table { RestaurantId = restaurants[1].RestaurantId, Capacity = 4 },
                new Table { RestaurantId = restaurants[2].RestaurantId, Capacity = 6 },
                new Table { RestaurantId = restaurants[3].RestaurantId, Capacity = 7 },
                new Table { RestaurantId = restaurants[4].RestaurantId, Capacity = 3 }
            });

        await context.Tables.AddRangeAsync(tables);
        await context.SaveChangesAsync();

        return tables;
    }
}
