using Microsoft.EntityFrameworkCore;
using RestaurantReservation.Db;
using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Services;

public class OrderOperations
{
    private readonly RestaurantReservationDbContext _context;

    public OrderOperations(RestaurantReservationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Order>> GetAllAsync() => await _context.Orders.ToListAsync();


    public async Task AddAsync(Order order)
    {
        _context.Orders.Add(order);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Order order)
    {
        var existingOrder = await _context.Orders.FindAsync(order.OrderId);
        if (existingOrder == null)
            return;

        _context.Entry(existingOrder).CurrentValues.SetValues(order);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var order = await _context.Orders.FindAsync(id);
        if (order is null) return;

        _context.Orders.Remove(order);
        await _context.SaveChangesAsync();
    }
}