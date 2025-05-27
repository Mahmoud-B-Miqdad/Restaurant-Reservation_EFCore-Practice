using AutoMapper;
using RestaurantReservation.Db.Entities;
using RestaurantReservation.Db.Repositories.Interfaces;
using RestaurantReservationSystem.API.DTOs.Requests;
using RestaurantReservationSystem.API.DTOs.Responses;
using RestaurantReservationSystem.API.Services.Interfaces;

namespace RestaurantReservationSystem.API.Services
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
            var orderItem = _mapper.Map<OrderItem>(request);
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
        public async Task<MenuItemResponse?> GetMenuItemAsync(int orderItemId)
        {
            var MenuItem = await _orderItemRepository.GetMenuItemByOrderItemIdAsync(orderItemId);
            return MenuItem == null ? null : _mapper.Map<MenuItemResponse>(MenuItem);
        }

        /// <inheritdoc />
        public async Task<OrderResponse?> GetOrderAsync(int orderItemId)
        {
            var order = await _orderItemRepository.GetOrderByOrderItemIdAsync(orderItemId);
            return order == null ? null : _mapper.Map<OrderResponse>(order);
        }
    }
}