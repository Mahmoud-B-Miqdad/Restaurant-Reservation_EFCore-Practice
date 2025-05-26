using Microsoft.EntityFrameworkCore;
using RestaurantReservation.Db.Entities;
using RestaurantReservationSystem.Domain.Interfaces.Repositories;

namespace RestaurantReservation.Db.Repositories
{
    internal class ReservationRepository : IReservationRepository
    {
        private readonly RestaurantReservationDbContext _context;

        public ReservationRepository(RestaurantReservationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Reservation>> GetReservationsByCustomerAsync(int customerId)
        {
            return await _context.Reservations
                                 .Where(r => r.CustomerId == customerId)
                                 .ToListAsync();
        }

        public async Task<List<Reservation>> GetAllAsync()
        {
            return await _context.Reservations.ToListAsync();
        }

        public async Task<Reservation> GetByIdAsync(int id)
        {
            return await _context.Reservations.FindAsync(id);
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

        public async Task<IEnumerable<Reservation>> GetByRestaurantIdAsync(int restaurantId)
        {
            return await _context.Reservations
                .Where(e => e.RestaurantId == restaurantId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Reservation>> GetReservationsByTableIdAsync(int tableId)
        {
            return await _context.Reservations
                .Where(o => o.TableId == tableId)
                .ToListAsync();
        }
    }
}