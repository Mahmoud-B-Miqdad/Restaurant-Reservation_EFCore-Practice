namespace RestaurantReservation.Db.Seeders;

public static class RestaurantReservationSeeder
{
    public static async Task SeedAsync(RestaurantReservationDbContext context)
    {
        var restaurantSeeder = new RestaurantSeeder();
        var tableSeeder = new TableSeeder();
        var employeeSeeder = new EmployeeSeeder();
        var customerSeeder = new CustomerSeeder();
        var reservationSeeder = new ReservationSeeder();
        var orderSeeder = new OrderSeeder();
        var menuItemSeeder = new MenuItemSeeder();
        var orderItemSeeder = new OrderItemSeeder();

        var restaurants = await restaurantSeeder.SeedAsync(context);
        var tables = await tableSeeder.SeedAsync(context, restaurants);
        var employees = await employeeSeeder.SeedAsync(context, restaurants);
        var customers = await customerSeeder.SeedAsync(context);
        var reservations = await reservationSeeder.SeedAsync(context, customers, restaurants, tables);
        var orders = await orderSeeder.SeedAsync(context, reservations, employees);
        var menuItems = await menuItemSeeder.SeedAsync(context, restaurants);
        await orderItemSeeder.SeedAsync(context, orders, menuItems);
    }
}