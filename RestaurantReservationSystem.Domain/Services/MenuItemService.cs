using AutoMapper;
using RestaurantReservationSystem.Domain.DTOs.Requests;
using RestaurantReservationSystem.Domain.DTOs.Responses;
using RestaurantReservationSystem.Domain.Interfaces.Repositories;
using RestaurantReservationSystem.Domain.Interfaces.Services;
using RestaurantReservationSystem.Domain.Models;

namespace RestaurantReservationSystem.Domain.Services
{
    /// <summary>
    /// Provides business logic and service methods for managing menuItem operations.
    /// </summary>
    public class MenuItemService : IMenuItemService
    {

        private readonly IMenuItemRepository _menuItemRepository;
        private readonly IOrderItemRepository _orderItemRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="MenuItemService"/> class.
        /// </summary>
        /// <param name="repository">The repository responsible for menuItem data access.</param>
        /// <param name="mapper">The mapper used to convert between entities and DTOs.</param>
        public MenuItemService(IMenuItemRepository repository, IMapper mapper, IOrderItemRepository orderItemRepository)
        {
            _menuItemRepository = repository;
            _mapper = mapper;
            _orderItemRepository = orderItemRepository;
        }

        /// <inheritdoc />
        public async Task<List<MenuItemResponse>> GetAllAsync()
        {
            var menuItem = await _menuItemRepository.GetAllAsync();
            return _mapper.Map<List<MenuItemResponse>>(menuItem);
        }

        /// <inheritdoc />
        public async Task<MenuItemResponse?> GetByIdAsync(int id)
        {
            var menuItem = await _menuItemRepository.GetByIdAsync(id);
            return menuItem == null ? null : _mapper.Map<MenuItemResponse>(menuItem);
        }

        /// <inheritdoc />
        public async Task<MenuItemResponse> CreateAsync(MenuItemRequest request)
        {
            var menuItem = _mapper.Map<MenuItemModel>(request);
            await _menuItemRepository.AddAsync(menuItem);
            return _mapper.Map<MenuItemResponse>(menuItem);
        }

        /// <inheritdoc />
        public async Task<MenuItemResponse?> UpdateAsync(int id, MenuItemRequest request)
        {
            var updatedMenuItem = await _menuItemRepository.GetByIdAsync(id);
            if (updatedMenuItem == null) return null;

            _mapper.Map(request, updatedMenuItem);
            await _menuItemRepository.UpdateAsync(updatedMenuItem);
            return _mapper.Map<MenuItemResponse>(updatedMenuItem);
        }

        /// <inheritdoc />
        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _menuItemRepository.GetByIdAsync(id);
            if (existing == null) return false;

            await _menuItemRepository.DeleteAsync(id);
            return true;
        }

        /// <inheritdoc />
        public async Task<IEnumerable<OrderItemResponse>> GetOrderItemsByMenuItamIdAsync(int menuItemId)
        {
            var orderItems = await _orderItemRepository.GetOrderItemsByMenuItemIdAsync(menuItemId);
            return _mapper.Map<IEnumerable<OrderItemResponse>>(orderItems);
        }

        /// <inheritdoc />
        public async Task<IEnumerable<MenuItemResponse>> GetOrderedMenuItemsAsync(int reservationId)
        {
            var orderedMenuItems = await _menuItemRepository.ListOrderedMenuItemsAsync(reservationId);
            return _mapper.Map<IEnumerable<MenuItemResponse>>(orderedMenuItems);
        }

        /// <inheritdoc />
        public async Task<List<MenuItemResponse>> GetMenuItemsByRestaurantIdAsync(int restaurantId)
        {
            var menuItem = await _menuItemRepository.GetMenuItemsByRestaurantIdAsync(restaurantId);
            return _mapper.Map<List<MenuItemResponse>>(menuItem);
        }
    }
}