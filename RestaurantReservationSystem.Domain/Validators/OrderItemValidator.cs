using RestaurantReservationSystem.Domain.Exceptions;
using RestaurantReservationSystem.Domain.Interfaces.Repositories;
using RestaurantReservationSystem.Domain.Models;

namespace RestaurantReservationSystem.Domain.Validators
{
    public class OrderItemValidator
    {
        private readonly IOrderItemRepository _orderItemRepository;

        public OrderItemValidator(IOrderItemRepository orderItemRepository)
        {
            _orderItemRepository = orderItemRepository;
        }
        public async Task<OrderItemModel> EnsureOrderItemExistsAsync(int orderItemId)

        {
            var orderItem = await _orderItemRepository.GetByIdAsync(orderItemId);
            if (orderItem == null)
                throw new NotFoundException($"OrderItem with ID {orderItemId} not found");

            return orderItem;
        }
    }
}