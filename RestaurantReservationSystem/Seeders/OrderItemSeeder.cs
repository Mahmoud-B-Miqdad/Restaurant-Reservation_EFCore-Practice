using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Db.Seeders;

public class OrderItemSeeder
{
    public async Task SeedAsync(RestaurantReservationDbContext context, List<Order> orders, List<MenuItem> menuItems)
    {
        var orderItems = new List<OrderItem>
        {
            new() { OrderId = orders[0].OrderId, ItemId = menuItems[0].ItemId, Quantity = 2 },
            new() { OrderId = orders[1].OrderId, ItemId = menuItems[1].ItemId, Quantity = 1 },
            new() { OrderId = orders[2].OrderId, ItemId = menuItems[2].ItemId, Quantity = 3 },
            new() { OrderId = orders[3].OrderId, ItemId = menuItems[3].ItemId, Quantity = 5 },
            new() { OrderId = orders[4].OrderId, ItemId = menuItems[4].ItemId, Quantity = 2 }
        };

        await context.OrderItems.AddRangeAsync(orderItems);
        await context.SaveChangesAsync();
    }
}