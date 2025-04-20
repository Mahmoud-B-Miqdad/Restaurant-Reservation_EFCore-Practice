using RestaurantReservation.Db;
using RestaurantReservation.Db.Models;
using Microsoft.EntityFrameworkCore;

public class TableOperations
{
    private readonly RestaurantReservationDbContext _context;

    public TableOperations(RestaurantReservationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Table>> GetAllAsync()
    {
        return await _context.Tables.ToListAsync();
    }

    public async Task AddAsync(Table table)
    {
        await _context.Tables.AddAsync(table);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Table table)
    {
        var existingTable = await _context.Tables.FindAsync(table.TableId);
        if (existingTable == null)
            return;

        _context.Entry(existingTable).CurrentValues.SetValues(table);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var table = await _context.Tables.FindAsync(id);
        if (table != null)
        {
            _context.Tables.Remove(table);
            await _context.SaveChangesAsync();
        }
    }
}