using AutoMapper;
using RestaurantReservationSystem.API.DTOs.Requests;
using RestaurantReservationSystem.Domain.DTOs.Responses;
using RestaurantReservationSystem.Domain.Interfaces.Repositories;
using RestaurantReservationSystem.Domain.Interfaces.Services;
using RestaurantReservationSystem.Domain.Models;

namespace RestaurantReservationSystem.Domain.Services
{
    /// <summary>
    /// Provides business logic and service methods for managing orderItem operations.
    /// </summary>
    public class OrderItemService : IOrderItemService
    {
        private readonly IOrderItemRepository _orderItemRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="OrderItemService"/> class.
        /// </summary>
        /// <param name="repository">The repository responsible for orderItem data access.</param>
        /// <param name="mapper">The mapper used to convert between entities and DTOs.</param>
        public OrderItemService(IOrderItemRepository repository, IMapper mapper)
        {
            _orderItemRepository = repository;
            _mapper = mapper;
        }

        /// <inheritdoc />
        public async Task<List<OrderItemResponse>> GetAllAsync()
        {
            var orderItem = await _orderItemRepository.GetAllAsync();
            return _mapper.Map<List<OrderItemResponse>>(orderItem);
        }

        /// <inheritdoc />
        public async Task<OrderItemResponse?> GetByIdAsync(int id)
        {
            var orderItem = await _orderItemRepository.GetByIdAsync(id);
            return orderItem == null ? null : _mapper.Map<OrderItemResponse>(orderItem);
        }

        /// <inheritdoc />
        public async Task<OrderItemResponse> CreateAsync(OrderItemRequest request)
        {
            var orderItem = _mapper.Map<OrderItemModel>(request);
            await _orderItemRepository.AddAsync(orderItem);
            return _mapper.Map<OrderItemResponse>(orderItem);
        }

        /// <inheritdoc />
        public async Task<OrderItemResponse?> UpdateAsync(int id, OrderItemRequest request)
        {
            var updatedorderItem = await _orderItemRepository.GetByIdAsync(id);
            if (updatedorderItem == null) return null;

            _mapper.Map(request, updatedorderItem);
            await _orderItemRepository.UpdateAsync(updatedorderItem);
            return _mapper.Map<OrderItemResponse>(updatedorderItem);
        }

        /// <inheritdoc />
        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _orderItemRepository.GetByIdAsync(id);
            if (existing == null) return false;

            await _orderItemRepository.DeleteAsync(id);
            return true;
        }

        /// <inheritdoc />
        public async Task<List<OrderItemResponse>> GetOrderItemsByMenuItamIdAsync(int menuItemId)
        {
            var orderItems = await _orderItemRepository.GetOrderItemsByMenuItemIdAsync(menuItemId);
            return _mapper.Map<List<OrderItemResponse>>(orderItems);
        }

        /// <inheritdoc />
        public async Task<List<OrderItemResponse>> GetOrderItemsByOrderIdAsync(int orderId)
        {
            var orders = await _orderItemRepository.GetOrderItemsByOrderIdAsync(orderId);
            return _mapper.Map<List<OrderItemResponse>>(orders);
        }
    }
}