using AutoMapper;
using RestaurantReservation.Db.Entities;
using RestaurantReservation.Db.Repositories.Interfaces;
using RestaurantReservationSystem.API.DTOs.Requests;
using RestaurantReservationSystem.API.DTOs.Responses;
using RestaurantReservationSystem.API.Services.Interfaces;

namespace RestaurantReservationSystem.API.Services
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
        public async Task<MenuItemResponse> CreateAsync(EmployeeRequest request)
        {
            var menuItem = _mapper.Map<MenuItem>(request);
            await _menuItemRepository.AddAsync(menuItem);
            return _mapper.Map<MenuItemResponse>(menuItem);
        }

        /// <inheritdoc />
        public async Task<MenuItemResponse?> UpdateAsync(int id, EmployeeRequest request)
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
        public async Task<IEnumerable<OrderItemResponse>> GetOrderItemsAsync(int menuItemId)
        {
            var orderItems = await _orderItemRepository.GetOrderItemsByMenuItemIdAsync(menuItemId);
            return _mapper.Map<IEnumerable<OrderItemResponse>>(orderItems);
        }

        /// <inheritdoc />
        public async Task<RestaurantResponse?> GetRestaurantAsync(int menuItemId)
        {
            var restaurant = await _menuItemRepository.GetRestaurantByMenuItemIdAsync(menuItemId);
            return _mapper.Map<RestaurantResponse>(restaurant);
        }

    }
}