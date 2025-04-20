using RestaurantReservation.Db.Models;
using RestaurantReservation.Db;
using Microsoft.EntityFrameworkCore;

public class ReservationOperations
{
    private readonly RestaurantReservationDbContext _context;

    public ReservationOperations(RestaurantReservationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Reservation>> GetAllAsync()
    {
        return await _context.Reservations.ToListAsync();
    }

    public async Task AddAsync(Reservation reservation)
    {
        await _context.Reservations.AddAsync(reservation);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Reservation reservation)
    {
        var existingReservation = await _context.Reservations.FindAsync(reservation.ReservationId);
        if (existingReservation == null)
            return;

        _context.Entry(existingReservation).CurrentValues.SetValues(reservation);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var reservation = await _context.Reservations.FindAsync(id);
        if (reservation != null)
        {
            _context.Reservations.Remove(reservation);
            await _context.SaveChangesAsync();
        }
    }
}
