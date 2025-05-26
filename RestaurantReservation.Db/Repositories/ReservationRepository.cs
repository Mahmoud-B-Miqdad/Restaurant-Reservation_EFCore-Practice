using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RestaurantReservation.Db.Entities;
using RestaurantReservationSystem.Domain.Interfaces.Repositories;
using RestaurantReservationSystem.Domain.Models;

namespace RestaurantReservation.Db.Repositories
{
    internal class ReservationRepository : IReservationRepository
    {
        private readonly RestaurantReservationDbContext _context;
        private readonly IMapper _mapper;

        public ReservationRepository(RestaurantReservationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<ReservationModel>> GetReservationsByCustomerAsync(int customerId)
        {
            var reservations = await _context.Reservations
                                             .Where(r => r.CustomerId == customerId)
                                             .ToListAsync();
            return _mapper.Map<List<ReservationModel>>(reservations);
        }

        public async Task<List<ReservationModel>> GetAllAsync()
        {
            var reservations = await _context.Reservations.ToListAsync();
            return _mapper.Map<List<ReservationModel>>(reservations);
        }

        public async Task<ReservationModel> GetByIdAsync(int id)
        {
            var reservation = await _context.Reservations.FindAsync(id);
            return _mapper.Map<ReservationModel>(reservation);
        }

        public async Task AddAsync(ReservationModel reservationModel)
        {
            var reservation = _mapper.Map<Reservation>(reservationModel);
            await _context.Reservations.AddAsync(reservation);
            await _context.SaveChangesAsync();

            reservationModel.ReservationId = reservation.ReservationId;
        }

        public async Task UpdateAsync(ReservationModel reservationModel)
        {
            var existing = await _context.Reservations.FindAsync(reservationModel.ReservationId);
            if (existing is null) return;

            _context.Entry(existing).CurrentValues.SetValues(_mapper.Map<Reservation>(reservationModel));
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var reservation = await _context.Reservations
                 .Include(r => r.Orders)
                 .FirstOrDefaultAsync(r => r.ReservationId == id);

            if (reservation.Orders.Any())
                throw new InvalidOperationException("Cannot delete reservation with existing orders.");

            if (reservation != null)
            {
                _context.Reservations.Remove(reservation);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<ReservationModel>> GetByRestaurantIdAsync(int restaurantId)
        {
            var reservations = await _context.Reservations
                                             .Where(r => r.RestaurantId == restaurantId)
                                             .ToListAsync();
            return _mapper.Map<List<ReservationModel>>(reservations);
        }


        public async Task<IEnumerable<ReservationModel>> GetReservationsByTableIdAsync(int tableId)
        {
            var reservations = await _context.Reservations
                                             .Where(r => r.TableId == tableId)
                                             .ToListAsync();
            return _mapper.Map<List<ReservationModel>>(reservations);
        }

        public async Task<Customer?> GetCustomerByReservationIdAsync(int reservationId)
        {
            var reservations = await _context.Reservations
                .Include(r => r.Customer)
                .FirstOrDefaultAsync(r => r.ReservationId == reservationId);

            return reservations?.Customer;
        }

        public async Task<Restaurant?> GetRestaurantByReservationIdAsync(int reservationId)
        {
            var reservations = await _context.Reservations
                .Include(r => r.Restaurant)
                .FirstOrDefaultAsync(r => r.ReservationId == reservationId);

            return reservations?.Restaurant;
        }

        public async Task<Table?> GetTableByReservationIdAsync(int reservationId)
        {
            var reservations = await _context.Reservations
                .Include(r => r.Table)
                .FirstOrDefaultAsync(r => r.ReservationId == reservationId);

            return reservations?.Table;
        }
    }
}