using Microsoft.EntityFrameworkCore;
using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Db.Repositories
{
    public class OrderRepository
    {
        private readonly RestaurantReservationDbContext _context;

        public OrderRepository(RestaurantReservationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Order>> ListOrdersAndMenuItemsAsync(int reservationId)
        {
            return await _context.Orders
                .Where(o => o.ReservationId == reservationId)
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.MenuItem)
                .ToListAsync();
        }
    }
}
