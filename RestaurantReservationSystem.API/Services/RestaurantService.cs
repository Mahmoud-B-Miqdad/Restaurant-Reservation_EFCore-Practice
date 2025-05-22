using AutoMapper;
using RestaurantReservation.Db.Entities;
using RestaurantReservation.Db.Repositories.Interfaces;
using RestaurantReservationSystem.API.DTOs.Requests;
using RestaurantReservationSystem.API.DTOs.Responses;
using RestaurantReservationSystem.API.Services.Interfaces;

namespace RestaurantReservationSystem.API.Services
{
    /// <summary>
    /// Implements business logic for managing restaurants.
    /// </summary>
    public class RestaurantService : IRestaurantService
    {
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ITableRepository _tableRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="RestaurantService"/> class.
        /// </summary>
        /// <param name="restaurantRepository">The restaurant repository.</param>
        /// <param name="mapper">The AutoMapper instance.</param>
        public RestaurantService(IRestaurantRepository restaurantRepository, IMapper mapper, IEmployeeRepository employeeRepository,
            ITableRepository tableRrepository)
        {
            _restaurantRepository = restaurantRepository;
            _mapper = mapper;
            _employeeRepository = employeeRepository;
            _tableRepository = tableRrepository;
        }

        /// <inheritdoc />
        public async Task<IEnumerable<RestaurantResponse>> GetAllAsync()
        {
            var restaurants = await _restaurantRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<RestaurantResponse>>(restaurants);
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
            var restaurant = _mapper.Map<Restaurant>(request);
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
        public async Task<IEnumerable<EmployeeResponse>> GetEmployeesAsync(int restaurantId)
        {
            var employees = await _employeeRepository.GetByRestaurantIdAsync(restaurantId);
            return _mapper.Map<IEnumerable<EmployeeResponse>>(employees);
        }


    }
}