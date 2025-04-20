using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Db.Seeders;

public class MenuItemSeeder
{
    public async Task<List<MenuItem>> SeedAsync(RestaurantReservationDbContext context, List<Restaurant> restaurants)
    {
        var menuItems = new List<MenuItem>
        {
            new() { Name = "Burger", Description = "Beef with cheese", Price = 10, RestaurantId = restaurants[0].RestaurantId },
            new() { Name = "Pasta", Description = "White sauce", Price = 12, RestaurantId = restaurants[1].RestaurantId },
            new() { Name = "Salad", Description = "Greek salad", Price = 8, RestaurantId = restaurants[2].RestaurantId },
            new() { Name = "Steak", Description = "Ribeye", Price = 22, RestaurantId = restaurants[3].RestaurantId },
            new() { Name = "Sushi", Description = "Mixed roll", Price = 18, RestaurantId = restaurants[4].RestaurantId }
        };

        await context.MenuItems.AddRangeAsync(menuItems);
        await context.SaveChangesAsync();

        return menuItems;
    }
}