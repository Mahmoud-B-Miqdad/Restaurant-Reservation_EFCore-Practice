using AutoMapper;
using RestaurantReservationSystem.API.DTOs.Requests;
using RestaurantReservationSystem.API.DTOs.Responses;
using RestaurantReservationSystem.Domain.Interfaces.Repositories;
using RestaurantReservationSystem.Domain.Interfaces.Services;
using RestaurantReservationSystem.Domain.Models;

namespace RestaurantReservationSystem.API.Services
{
    /// <summary>
    /// Implements business logic for managing restaurants.
    /// </summary>
    public class RestaurantService : IRestaurantService
    {
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly IMenuItemRepository _menuItemRepository;
        private readonly IReservationRepository _reservationRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="RestaurantService"/> class.
        /// </summary>
        /// <param name="restaurantRepository">The restaurant repository.</param>
        /// <param name="mapper">The AutoMapper instance.</param>
        public RestaurantService(IRestaurantRepository restaurantRepository, IMapper mapper,
            IMenuItemRepository menuItemRepository, IReservationRepository reservationRepository)
        {
            _restaurantRepository = restaurantRepository;
            _mapper = mapper;
            _menuItemRepository = menuItemRepository;
            _reservationRepository = reservationRepository;
        }

        /// <inheritdoc />
        public async Task<List<RestaurantResponse>> GetAllAsync()
        {
            var restaurants = await _restaurantRepository.GetAllAsync();
            return _mapper.Map<List<RestaurantResponse>>(restaurants);
        }

        /// <inheritdoc />
        public async Task<RestaurantResponse> GetByIdAsync(int id)
        {
            var restaurant = await _restaurantRepository.GetByIdAsync(id);
            return _mapper.Map<RestaurantResponse>(restaurant);
        }

        /// <inheritdoc />
        public async Task<RestaurantResponse> CreateAsync(RestaurantRequest request)
        {
            var restaurant = _mapper.Map<RestaurantModel>(request);
            await _restaurantRepository.AddAsync(restaurant);
            return _mapper.Map<RestaurantResponse>(restaurant);
        }

        /// <inheritdoc />
        public async Task<RestaurantResponse?> UpdateAsync(int id, RestaurantRequest request)
        {
            var existing = await _restaurantRepository.GetByIdAsync(id);
            if (existing == null) return null;

            var updated = _mapper.Map(request, existing);
            await _restaurantRepository.UpdateAsync(updated);
            return _mapper.Map<RestaurantResponse>(updated);
        }

        /// <inheritdoc />
        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _restaurantRepository.GetByIdAsync(id);
            if (existing == null) return false;

            await _restaurantRepository.DeleteAsync(id);
            return true;
        }

        /// <inheritdoc />
        public async Task<List<MenuItemResponse>> GetMenuItemsByRestaurantIdAsync(int restaurantId)
        {
            var menuItem = await _menuItemRepository.GetByRestaurantIdAsync(restaurantId);
            return _mapper.Map<List<MenuItemResponse>>(menuItem);
        }

        /// <inheritdoc />
        public async Task<List<ReservationResponse>> GetReservationsByRestaurantIdAsync(int restaurantId)
        {
            var reservation = await _reservationRepository.GetByRestaurantIdAsync(restaurantId);
            return _mapper.Map<List<ReservationResponse>>(reservation);
        }

        /// <inheritdoc />
        public async Task<RestaurantResponse?> GetRestaurantByEmployeeIdAsync(int employeeId)
        {
            var restaurant = await _restaurantRepository.GetRestaurantByEmployeeIdAsync(employeeId);
            return restaurant == null ? null : _mapper.Map<RestaurantResponse>(restaurant);
        }

        public async Task<RestaurantResponse?> GetRestaurantByTableIdAsync(int tableId)
        {
            var restaurant = await _restaurantRepository.GetRestaurantByTableIdAsync(tableId);
            return restaurant == null ? null : _mapper.Map<RestaurantResponse>(restaurant);
        }
    }
}