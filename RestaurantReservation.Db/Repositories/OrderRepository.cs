using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RestaurantReservation.Db.Entities;
using RestaurantReservationSystem.Domain.Interfaces.Repositories;
using RestaurantReservationSystem.Domain.Models;

namespace RestaurantReservation.Db.Repositories
{
    internal class OrderRepository : IOrderRepository
    {
        private readonly RestaurantReservationDbContext _context;
        private readonly IMapper _mapper;

        public OrderRepository(RestaurantReservationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<OrderModel>> ListOrdersAndMenuItemsAsync(int reservationId)
        {
            var orders = await _context.Orders
                .Where(o => o.ReservationId == reservationId)
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.MenuItem)
                .ToListAsync();

            return _mapper.Map<List<OrderModel>>(orders);
        }


        public async Task<decimal> CalculateAverageOrderAmountAsync(int employeeId)
        {
            var employeeOrders = await _context.Orders
                .Where(o => o.EmployeeId == employeeId)
                .ToListAsync();

            if (!employeeOrders.Any())
                return 0;

            return employeeOrders.Average(o => o.TotalAmount);
        }

        public async Task<List<OrderModel>> GetAllAsync()
        {
            var orders = await _context.Orders.ToListAsync();
            return _mapper.Map<List<OrderModel>>(orders);
        }


        public async Task<OrderModel> GetByIdAsync(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            return _mapper.Map<OrderModel>(order);
        }

        public async Task AddAsync(OrderModel orderModel)
        {
            var order = _mapper.Map<Order>(orderModel);
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            orderModel.OrderId = order.OrderId;
        }


        public async Task UpdateAsync(OrderModel orderModel)
        {
            var existingOrder = await _context.Orders.FindAsync(orderModel.OrderId);
            if (existingOrder == null)
                return;

            _context.Entry(existingOrder).CurrentValues.SetValues(_mapper.Map<Order>(orderModel));
            await _context.SaveChangesAsync();
        }


        public async Task DeleteAsync(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order is null) return;

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<OrderModel>> GetOrdersByEmployeeIdAsync(int employeeId)
        {
            var orders = await _context.Orders
                .Where(o => o.EmployeeId == employeeId)
                .ToListAsync();

            return _mapper.Map<IEnumerable<OrderModel>>(orders);
        }
    }
}