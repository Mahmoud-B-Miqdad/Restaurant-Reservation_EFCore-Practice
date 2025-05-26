using AutoMapper;
using RestaurantReservationSystem.API.DTOs.Requests;
using RestaurantReservationSystem.API.DTOs.Responses;
using RestaurantReservationSystem.API.Interfaces;
using RestaurantReservationSystem.Domain.Interfaces.Repositories;

namespace RestaurantReservationSystem.API.Services
{
    /// <summary>
    /// Provides business logic and data manipulation for table-related operations.
    /// </summary>
    public class TableService : ITableService
    {
        private readonly ITableRepository _tableRepository;
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly IReservationRepository _reservationRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="TableService"/> class.
        /// </summary>
        /// <param name="tableRepository">The repository for table data access.</param>
        /// <param name="mapper">The AutoMapper instance for mapping entities to DTOs.</param>
        /// <param name="reservationRepository">The repository for reservation data access.</param>

        public TableService(ITableRepository tableRepository, IMapper mapper, IReservationRepository reservationRepository, IRestaurantRepository restaurantRepository)
        {
            _tableRepository = tableRepository;
            _mapper = mapper;
            _reservationRepository = reservationRepository;
            _restaurantRepository = restaurantRepository;
        }

        /// <inheritdoc />
        public async Task<IEnumerable<TableResponse>> GetAllAsync()
        {
            var tables = await _tableRepository.GetAllAsync();
            return _mapper.Map<List<TableResponse>>(tables);
        }

        /// <inheritdoc />
        public async Task<TableResponse?> GetByIdAsync(int id)
        {
            var table = await _tableRepository.GetByIdAsync(id);
            return table == null ? null : _mapper.Map<TableResponse>(table);
        }

        /// <inheritdoc />
        public async Task<TableResponse> CreateAsync(TableRequest request)
        {
            var table = _mapper.Map<Table>(request);
            await _tableRepository.AddAsync(table);
            return _mapper.Map<TableResponse>(table);
        }

        /// <inheritdoc />
        public async Task<TableResponse?> UpdateAsync(int id, TableRequest request)
        {
            var updatedTable = await _tableRepository.GetByIdAsync(id);
            if (updatedTable == null) return null;

            _mapper.Map(request, updatedTable);
            await _tableRepository.UpdateAsync(updatedTable);
            return _mapper.Map<TableResponse>(updatedTable);
        }

        /// <inheritdoc />
        public async Task<bool> DeleteAsync(int id)
        {
            var existingTable = await _tableRepository.GetByIdAsync(id);
            if (existingTable == null) return false;

            await _tableRepository.DeleteAsync(id);
            return true;
        }

        /// <inheritdoc />
        public async Task<RestaurantResponse?> GetRestaurantAsync(int tableId)
        {
            var restaurant = await _restaurantRepository.GetRestaurantByTableIdAsync(tableId);
            return restaurant == null ? null : _mapper.Map<RestaurantResponse>(restaurant);
        }

        /// <inheritdoc />
        public async Task<List<ReservationResponse>> GetReservationsAsync(int tableId)
        {
            var orders = await _reservationRepository.GetReservationsByTableIdAsync(tableId);
            return _mapper.Map<List<ReservationResponse>>(orders);
        }

    }
}