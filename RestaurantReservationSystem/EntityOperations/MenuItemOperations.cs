using Microsoft.EntityFrameworkCore;
using RestaurantReservation.Db;
using RestaurantReservation.Db.Models;

public class MenuItemOperations
{
    private readonly RestaurantReservationDbContext _context;

    public MenuItemOperations(RestaurantReservationDbContext context)
    {
        _context = context;
    }

    public async Task<List<MenuItem>> GetAllAsync() => await _context.MenuItems.ToListAsync();

    public async Task AddAsync(MenuItem menuItem)
    {
        _context.MenuItems.Add(menuItem);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(MenuItem menuItem)
    {
        var existingMenuItem = await _context.MenuItems.FindAsync(menuItem.ItemId);
        if (existingMenuItem == null)
            return;

        _context.Entry(existingMenuItem).CurrentValues.SetValues(menuItem);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var menuItem = await _context.MenuItems.FindAsync(id);
        if (menuItem is null) return;

        _context.MenuItems.Remove(menuItem);
        await _context.SaveChangesAsync();
    }
}