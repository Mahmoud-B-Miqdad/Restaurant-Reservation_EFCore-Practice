using AutoMapper;
using RestaurantReservationSystem.Domain.DTOs.Requests;
using RestaurantReservationSystem.Domain.DTOs.Responses;
using RestaurantReservationSystem.Domain.Interfaces.Repositories;
using RestaurantReservationSystem.Domain.Interfaces.Services;
using RestaurantReservationSystem.Domain.Models;

namespace RestaurantReservationSystem.Domain.Services
{
    /// <summary>
    /// Provides business logic for managing orders, including CRUD operations and related data retrieval
    /// such as associated reservation, employee, and order items.
    /// </summary>
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderItemRepository _orderItemRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="OrderService"/> class.
        /// </summary>
        /// <param name="orderRepository">Repository for accessing order data.</param>
        /// <param name="mapper">AutoMapper instance for mapping between entities and DTOs.</param>
        /// <param name="orderItemRepository">Repository for accessing order item data.</param>

        public OrderService(IOrderRepository orderRepository, IMapper mapper, IOrderItemRepository orderItemRepository)
        {
            _orderRepository = orderRepository;
            _orderItemRepository = orderItemRepository;
            _mapper = mapper;
        }

        /// <inheritdoc />
        public async Task<List<OrderResponse>> GetAllAsync()
        {
            var orders = await _orderRepository.GetAllAsync();
            return _mapper.Map<List<OrderResponse>>(orders);
        }

        /// <inheritdoc />
        public async Task<OrderResponse?> GetByIdAsync(int id)
        {
            var order = await _orderRepository.GetByIdAsync(id);
            return order == null ? null : _mapper.Map<OrderResponse>(order);
        }

        /// <inheritdoc />
        public async Task<OrderResponse?> CreateAsync(OrderRequest request)
        {
            var order = _mapper.Map<OrderModel>(request);
            await _orderRepository.AddAsync(order);
            return _mapper.Map<OrderResponse>(order);
        }

        /// <inheritdoc />
        public async Task<OrderResponse?> UpdateAsync(int id, OrderRequest request)
        {
            var updatedOrder = await _orderRepository.GetByIdAsync(id);
            if (updatedOrder == null) return null;

            _mapper.Map(request, updatedOrder);
            await _orderRepository.UpdateAsync(updatedOrder);
            return _mapper.Map<OrderResponse>(updatedOrder);
        }

        /// <inheritdoc />
        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _orderRepository.GetOrderByIdWithOrderItemsAsync(id);
            if (existing == null) return false;

            if (existing.OrderItems.Any())
                throw new InvalidOperationException("Cannot delete order with existing order items.");

            await _orderRepository.DeleteAsync(id);
            return true;
        }

        /// <inheritdoc />
        public async Task<List<OrderItemResponse>> GetOrderItemsAsync(int orderId)
        {
            var orders = await _orderItemRepository.GetOrderItemsByOrderIdAsync(orderId);
            return _mapper.Map<List<OrderItemResponse>>(orders);
        }

        /// <inheritdoc />
        public async Task<List<OrderResponse>> GetOrdersByEmployeeIdAsync(int employeeId)
        {
            var orders = await _orderRepository.GetOrdersByEmployeeIdAsync(employeeId);
            return _mapper.Map<List<OrderResponse>>(orders);
        }

        /// <inheritdoc />
        public async Task<List<OrderResponse>> GetOrdersByReservationIdAsync(int reservationId)
        {
            var orders = await _orderRepository.GetOrdersByReservationIdAsync(reservationId);
            return _mapper.Map<List<OrderResponse>>(orders);
        }
    }
}