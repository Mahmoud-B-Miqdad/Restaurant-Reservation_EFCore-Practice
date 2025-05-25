using RestaurantReservation.Db.Entities;
using RestaurantReservation.Db;
using Microsoft.EntityFrameworkCore;
using RestaurantReservation.Db.Repositories.Interfaces;

internal class RestaurantRepository : IRestaurantRepository
{
    private readonly RestaurantReservationDbContext _context;

    public RestaurantRepository(RestaurantReservationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Restaurant>> GetAllAsync()
    {
        return await _context.Restaurants.ToListAsync();
    }

    public async Task<Restaurant> GetByIdAsync(int id)
    {
        return await _context.Restaurants.FindAsync(id);
    }


    public async Task AddAsync(Restaurant restaurant)
    {
        await _context.Restaurants.AddAsync(restaurant);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Restaurant restaurant)
    {
        var existingRestaurant = await _context.Restaurants.FindAsync(restaurant.RestaurantId);
        if (existingRestaurant == null)
            return;

        _context.Entry(existingRestaurant).CurrentValues.SetValues(restaurant);
        await _context.SaveChangesAsync();
    }


    public async Task DeleteAsync(int id)
    {
        var restaurant = await _context.Restaurants.FindAsync(id);
        if (restaurant != null)
        {
            _context.Restaurants.Remove(restaurant);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<Restaurant?> GetRestaurantByEmployeeIdAsync(int employeeId)
    {
        var employee = await _context.Employees
            .Include(e => e.Restaurant)
            .FirstOrDefaultAsync(e => e.EmployeeId == employeeId);

        return employee?.Restaurant;
    }

    public async Task<Restaurant?> GetRestaurantByTableIdAsync(int tableId)
    {
        var table = await _context.Tables
            .Include(e => e.Restaurant)
            .FirstOrDefaultAsync(e => e.TableId == tableId);

        return table?.Restaurant;
    }
}