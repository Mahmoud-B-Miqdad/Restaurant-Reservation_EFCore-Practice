using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Db.Seeders;

public class EmployeeSeeder
{
    public async Task<List<Employee>> SeedAsync(RestaurantReservationDbContext context, List<Restaurant> restaurants)
    {
        var employees = new List<Employee>
        {
            new() { FirstName = "Ali", LastName = "Hassan", Position = "Manager", RestaurantId = restaurants[0].RestaurantId },
            new() { FirstName = "Sara", LastName = "Kamal", Position = "Chef", RestaurantId = restaurants[1].RestaurantId },
            new() { FirstName = "Omar", LastName = "Salem", Position = "Waiter", RestaurantId = restaurants[2].RestaurantId },
            new() { FirstName = "Nora", LastName = "Yousef", Position = "Manager", RestaurantId = restaurants[3].RestaurantId },
            new() { FirstName = "Khaled", LastName = "Fathy", Position = "Waiter", RestaurantId = restaurants[4].RestaurantId },
        };

        await context.Employees.AddRangeAsync(employees);
        await context.SaveChangesAsync();

        return employees;
    }
}