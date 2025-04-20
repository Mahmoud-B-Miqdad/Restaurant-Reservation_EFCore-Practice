using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Db.Seeders;

public class OrderSeeder
{
    public async Task<List<Order>> SeedAsync(RestaurantReservationDbContext context, List<Reservation> reservations, List<Employee> employees)
    {
        var orders = new List<Order>
        {
            new() { ReservationId = reservations[0].ReservationId, EmployeeId = employees[0].EmployeeId, OrderDate = DateTime.Now.AddHours(-1), TotalAmount = 25.5m },
            new() { ReservationId = reservations[1].ReservationId, EmployeeId = employees[1].EmployeeId, OrderDate = DateTime.Now.AddHours(-2), TotalAmount = 40m },
            new() { ReservationId = reservations[2].ReservationId, EmployeeId = employees[2].EmployeeId, OrderDate = DateTime.Now.AddHours(-3), TotalAmount = 55m },
            new() { ReservationId = reservations[3].ReservationId, EmployeeId = employees[3].EmployeeId, OrderDate = DateTime.Now.AddHours(-4), TotalAmount = 30m },
            new() { ReservationId = reservations[4].ReservationId, EmployeeId = employees[4].EmployeeId, OrderDate = DateTime.Now.AddHours(-5), TotalAmount = 65m }
        };

        await context.Orders.AddRangeAsync(orders);
        await context.SaveChangesAsync();

        return orders;
    }
}