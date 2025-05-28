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
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ITableRepository _tableRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="RestaurantService"/> class.
        /// </summary>
        /// <param name="restaurantRepository">The restaurant repository.</param>
        /// <param name="mapper">The AutoMapper instance.</param>
        public RestaurantService(IRestaurantRepository restaurantRepository, IMapper mapper,
            IMenuItemRepository menuItemRepository, IReservationRepository reservationRepository,
            IEmployeeRepository employeeRepository, ITableRepository tableRepository)
        {
            _restaurantRepository = restaurantRepository;
            _mapper = mapper;
            _menuItemRepository = menuItemRepository;
            _reservationRepository = reservationRepository;
            _employeeRepository = employeeRepository;
            _tableRepository = tableRepository;
        }
        private async Task<RestaurantModel> EnsureRestaurantExistsAsync(int restaurantId)

        {
            var restaurant = await _restaurantRepository.GetByIdAsync(restaurantId);
            if (restaurant == null)
                throw new NotFoundException($"Restaurant with ID {restaurantId} not found");

            return restaurant;
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
            var restaurant = await EnsureRestaurantExistsAsync(id);
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
            var existingRestaurant = await EnsureRestaurantExistsAsync(id);

            var updated = _mapper.Map(request, existingRestaurant);
            await _restaurantRepository.UpdateAsync(updated);
            return _mapper.Map<RestaurantResponse>(updated);
        }

        /// <inheritdoc />
        public async Task<bool> DeleteAsync(int id)
        {
            var existingRestaurant = await EnsureRestaurantExistsAsync(id);
            await _restaurantRepository.DeleteAsync(id);
            return true;
        }

        /// <inheritdoc />
        public async Task<List<MenuItemResponse>> GetMenuItemsByRestaurantIdAsync(int restaurantId)
        {
            var restaurant = await EnsureRestaurantExistsAsync(restaurantId);

            var menuItems = await _menuItemRepository.GetByRestaurantIdAsync(restaurantId);
            return _mapper.Map<List<MenuItemResponse>>(menuItems);
        }

        /// <inheritdoc />
        public async Task<List<ReservationResponse>> GetReservationsByRestaurantIdAsync(int restaurantId)
        {
            var restaurant = await EnsureRestaurantExistsAsync(restaurantId);

            var reservations = await _reservationRepository.GetByRestaurantIdAsync(restaurantId);
            return _mapper.Map<List<ReservationResponse>>(reservations);
        }

        /// <inheritdoc />
        public async Task<RestaurantResponse?> GetRestaurantByEmployeeIdAsync(int employeeId)
        {
            var employee = await _employeeRepository.GetByIdAsync(employeeId);
            if (employee == null)
                throw new NotFoundException($"Employee with ID {employeeId} not found");

            var restaurant = await _restaurantRepository.GetRestaurantByEmployeeIdAsync(employeeId);
            return employee == null ? null : _mapper.Map<RestaurantResponse>(employee);
        }

        public async Task<RestaurantResponse?> GetRestaurantByTableIdAsync(int tableId)
        {
            var table = await _tableRepository.GetByIdAsync(tableId);
            if (table == null)
                throw new NotFoundException($"Table with ID {tableId} not found");

            var restaurant = await _restaurantRepository.GetRestaurantByTableIdAsync(tableId);
            return restaurant == null ? null : _mapper.Map<RestaurantResponse>(restaurant);
        }
    }
}