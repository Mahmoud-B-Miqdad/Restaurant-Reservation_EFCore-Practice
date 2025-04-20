using Microsoft.EntityFrameworkCore;
using RestaurantReservation.Db;
using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Services;

public class OrderItemOperations
{
    private readonly RestaurantReservationDbContext _context;

    public OrderItemOperations(RestaurantReservationDbContext context)
    {
        _context = context;
    }

    public async Task<List<OrderItem>> GetAllAsync() => await _context.OrderItems.ToListAsync();

    public async Task AddAsync(OrderItem orderItem)
    {
        _context.OrderItems.Add(orderItem);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(OrderItem orderItem)
    {
        var existingOrderItem = await _context.Restaurants.FindAsync(orderItem.OrderItemId);
        if (existingOrderItem == null)
            return;

        _context.Entry(existingOrderItem).CurrentValues.SetValues(orderItem);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var orderItem = await _context.OrderItems.FindAsync(id);
        if (orderItem is null) return;

        _context.OrderItems.Remove(orderItem);
        await _context.SaveChangesAsync();
    }
}
