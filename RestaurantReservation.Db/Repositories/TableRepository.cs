using RestaurantReservation.Db;
using RestaurantReservation.Db.Entities;
using Microsoft.EntityFrameworkCore;
using RestaurantReservation.Db.Repositories.Interfaces;

internal class TableRepository : ITableRepository
{
    private readonly RestaurantReservationDbContext _context;

    public TableRepository(RestaurantReservationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Table>> GetAllAsync()
    {
        return await _context.Tables.ToListAsync();
    }

    public async Task<Table> GetByIdAsync(int id)
    {
        return await _context.Tables.FindAsync(id);
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

    public async Task<IEnumerable<Table>> GetByRestaurantIdAsync(int restaurantId)
    {
        return await _context.Tables
            .Where(e => e.RestaurantId == restaurantId)
            .ToListAsync();
    }

    public async Task<Restaurant?> GetRestaurantByTableIdAsync(int tableId)
    {
        var table = await _context.Tables
            .Include(e => e.Restaurant)
            .FirstOrDefaultAsync(e => e.TableId == tableId);

        return table?.Restaurant;
    }
}