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
        private readonly IMenuItemService _menuItemService;
        private readonly IReservationService _reservationService;
        private readonly IEmployeeService _employeeService;
        private readonly ITableService _tableService;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="RestaurantService"/> class.
        /// </summary>
        /// <param name="restaurantRepository">The restaurant repository.</param>
        /// <param name="mapper">The AutoMapper instance.</param>
        public RestaurantService(IRestaurantRepository restaurantRepository, IMapper mapper,
            IMenuItemService menuItemService, IReservationService reservationService,
            IEmployeeService employeeService, ITableService tableService)
        {
            _restaurantRepository = restaurantRepository;
            _mapper = mapper;
            _menuItemService = menuItemService;
            _reservationService = reservationService;
            _employeeService = employeeService;
            _tableService = tableService;
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
        public async Task<RestaurantResponse?> GetRestaurantByMenuItamIdAsync(int menuItemId)
        {
            var menuItem = await _menuItemService.GetByIdAsync(menuItemId);
            if (menuItem == null)
                throw new NotFoundException($"MenuItem with ID {menuItemId} not found");

            var restaurant = await _restaurantRepository.GetRestaurantByMenuItemIdAsync(menuItemId);
            return _mapper.Map<RestaurantResponse>(restaurant);
        }

        public async Task<RestaurantResponse?> GetRestaurantByReservationIdAsync(int reservationId)
        {
            var reservation = await _reservationService.GetByIdAsync(reservationId);
            if (reservation == null)
                throw new NotFoundException($"Reservation with ID {reservationId} not found");

            var restaurant = await _restaurantRepository.GetRestaurantByReservationIdAsync(reservationId);
            return restaurant == null ? null : _mapper.Map<RestaurantResponse>(restaurant);
        }

        public async Task<RestaurantResponse?> GetRestaurantByTableIdAsync(int tableId)
        {
            var table = await _tableService.GetByIdAsync(tableId);
            if (table == null)
                throw new NotFoundException($"Table with ID {tableId} not found");

            var restaurant = await _restaurantRepository.GetRestaurantByTableIdAsync(tableId);
            return restaurant == null ? null : _mapper.Map<RestaurantResponse>(restaurant);
        }

        /// <inheritdoc />
        public async Task<RestaurantResponse?> GetRestaurantByEmployeeIdAsync(int employeeId)
        {
            var employee = await _employeeService.GetByIdAsync(employeeId);
            if (employee == null)
                throw new NotFoundException($"Employee with ID {employeeId} not found");

            var restaurant = await _restaurantRepository.GetRestaurantByEmployeeIdAsync(employeeId);
            return employee == null ? null : _mapper.Map<RestaurantResponse>(employee);
        }

        private async Task<RestaurantModel> EnsureRestaurantExistsAsync(int restaurantId)

        {
            var restaurant = await _restaurantRepository.GetByIdAsync(restaurantId);
            if (restaurant == null)
                throw new NotFoundException($"Restaurant with ID {restaurantId} not found");

            return restaurant;
        }
    }
}