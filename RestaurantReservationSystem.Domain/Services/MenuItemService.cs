using AutoMapper;
using RestaurantReservationSystem.Domain.DTOs.Requests;
using RestaurantReservationSystem.Domain.DTOs.Responses;
using RestaurantReservationSystem.Domain.Exceptions;
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
        private readonly IReservationService _reservationService;
        private readonly IRestaurantService _restaurantService;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="MenuItemService"/> class.
        /// </summary>
        /// <param name="repository">The repository responsible for menuItem data access.</param>
        /// <param name="mapper">The mapper used to convert between entities and DTOs.</param>
        public MenuItemService(IMenuItemRepository repository, IMapper mapper, IOrderItemRepository orderItemRepository,
            IReservationService reservationService, IRestaurantService restaurantService)
        {
            _menuItemRepository = repository;
            _mapper = mapper;
            _orderItemRepository = orderItemRepository;
            _reservationService = reservationService;
            _restaurantService = restaurantService;
        }

        private async Task<MenuItemModel> EnsureMenuItemExistsAsync(int menuItemId)

        {
            var menuItem = await _menuItemRepository.GetByIdAsync(menuItemId);
            if (menuItem == null)
                throw new NotFoundException($"MenuItem with ID {menuItemId} not found");

            return menuItem;
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
            var menuItem = await EnsureMenuItemExistsAsync(id);
            return _mapper.Map<MenuItemResponse>(menuItem);
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
            var updatedMenuItem = await EnsureMenuItemExistsAsync(id);

            _mapper.Map(request, updatedMenuItem);
            await _menuItemRepository.UpdateAsync(updatedMenuItem);
            return _mapper.Map<MenuItemResponse>(updatedMenuItem);
        }

        /// <inheritdoc />
        public async Task<bool> DeleteAsync(int id)
        {
            await EnsureMenuItemExistsAsync(id);
            var existingMenuItem = await _menuItemRepository.GetByIdWithOrderItemsAsync(id);

            if (existingMenuItem.OrderItems.Any())
                throw new InvalidOperationException("Cannot delete menu item with existing order items.");

            await _menuItemRepository.DeleteAsync(id);
            return true;
        }

        /// <inheritdoc />
        public async Task<List<OrderItemResponse>> GetOrderItemsByMenuItamIdAsync(int menuItemId)
        {
            var menuItem = await EnsureMenuItemExistsAsync(menuItemId);
            var orderItems = await _orderItemRepository.GetOrderItemsByMenuItemIdAsync(menuItemId);
            return _mapper.Map<List<OrderItemResponse>>(orderItems);
        }

        /// <inheritdoc />
        public async Task<List<MenuItemResponse>> GetOrderedMenuItemsAsync(int reservationId)
        {
            var reservation = await _reservationService.GetByIdAsync(reservationId);
            if (reservation == null)
                throw new NotFoundException($"Reservation with ID {reservationId} not found");

            var orderedMenuItems = await _menuItemRepository.ListOrderedMenuItemsAsync(reservationId);
            return _mapper.Map<List<MenuItemResponse>>(orderedMenuItems);
        }

        /// <inheritdoc />
        public async Task<List<MenuItemResponse>> GetMenuItemsByRestaurantIdAsync(int restaurantId)
        {
            var restaurant = await _restaurantService.GetByIdAsync(restaurantId);
            if (restaurant == null)
                throw new NotFoundException($"Restaurant with ID {restaurantId} not found");

            var menuItem = await _menuItemRepository.GetMenuItemsByRestaurantIdAsync(restaurantId);
            return _mapper.Map<List<MenuItemResponse>>(menuItem);
        }
    }
}