using RestaurantReservation.Db.Models;
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
}