using RestaurantReservationSystem.Domain.Exceptions;
using RestaurantReservationSystem.Domain.Interfaces.Repositories;
using RestaurantReservationSystem.Domain.Models;

namespace RestaurantReservationSystem.Domain.Validators
{
    public class OrderValidator
    {
        private readonly IOrderRepository _orderRepository;
        public OrderValidator(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public async Task<OrderModel> EnsureOrderExistsAsync(int orderId)

        {
            var order = await _orderRepository.GetByIdAsync(orderId);
            if (order == null)
                throw new NotFoundException($"Order with ID {orderId} not found");

            return order;
        }
    }
}