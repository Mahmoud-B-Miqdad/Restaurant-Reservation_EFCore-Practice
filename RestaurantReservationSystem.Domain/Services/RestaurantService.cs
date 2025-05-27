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
            var restaurant = await _restaurantRepository.GetByIdAsync(id)
                              ?? throw new NotFoundException($"Restaurant with ID {id} not found");
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
        public async Task<RestaurantResponse> UpdateAsync(int id, RestaurantRequest request)
        {
            var existing = await _restaurantRepository.GetByIdAsync(id)
                           ?? throw new NotFoundException($"Restaurant with ID {id} not found");

            var updated = _mapper.Map(request, existing);
            await _restaurantRepository.UpdateAsync(updated);
            return _mapper.Map<RestaurantResponse>(updated);
        }

        /// <inheritdoc />
        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _restaurantRepository.GetByIdAsync(id)
                           ?? throw new NotFoundException($"Restaurant with ID {id} not found");

            await _restaurantRepository.DeleteAsync(id);
            return true;
        }

        /// <inheritdoc />
        public async Task<List<MenuItemResponse>> GetMenuItemsByRestaurantIdAsync(int restaurantId)
        {
            _ = await _restaurantRepository.GetByIdAsync(restaurantId)
                ?? throw new NotFoundException($"Restaurant with ID {restaurantId} not found");

            var menuItems = await _menuItemRepository.GetByRestaurantIdAsync(restaurantId);
            return _mapper.Map<List<MenuItemResponse>>(menuItems);
        }

        /// <inheritdoc />
        public async Task<List<ReservationResponse>> GetReservationsByRestaurantIdAsync(int restaurantId)
        {
            _ = await _restaurantRepository.GetByIdAsync(restaurantId)
                ?? throw new NotFoundException($"Restaurant with ID {restaurantId} not found");

            var reservations = await _reservationRepository.GetByRestaurantIdAsync(restaurantId);
            return _mapper.Map<List<ReservationResponse>>(reservations);
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