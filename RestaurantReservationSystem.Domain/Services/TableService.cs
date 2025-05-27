using AutoMapper;
using RestaurantReservationSystem.Domain.DTOs.Requests;
using RestaurantReservationSystem.Domain.DTOs.Responses;
using RestaurantReservationSystem.Domain.Interfaces.Repositories;
using RestaurantReservationSystem.Domain.Interfaces.Services;
using RestaurantReservationSystem.Domain.Models;

namespace RestaurantReservationSystem.Domain.Services
{
    /// <summary>
    /// Provides business logic and data manipulation for table-related operations.
    /// </summary>
    public class TableService : ITableService
    {
        private readonly ITableRepository _tableRepository;
        private readonly IReservationRepository _reservationRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="TableService"/> class.
        /// </summary>
        /// <param name="tableRepository">The repository for table data access.</param>
        /// <param name="mapper">The AutoMapper instance for mapping entities to DTOs.</param>
        /// <param name="reservationRepository">The repository for reservation data access.</param>

        public TableService(ITableRepository tableRepository, IMapper mapper, IReservationRepository reservationRepository)
        {
            _tableRepository = tableRepository;
            _mapper = mapper;
            _reservationRepository = reservationRepository;
        }

        /// <inheritdoc />
        public async Task<List<TableResponse>> GetAllAsync()
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
            var table = _mapper.Map<TableModel>(request);
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
        public async Task<List<TableResponse>> GetTablesByRestaurantIdAsync(int restaurantId)
        {
            var tables = await _tableRepository.GetByRestaurantIdAsync(restaurantId);
            return _mapper.Map<List<TableResponse>>(tables);
        }

        /// <inheritdoc />
        public async Task<TableResponse?> GetTableByReservationIdAsync(int reservationId)
        {
            var table = await _tableRepository.GetTableByReservationIdAsync(reservationId);
            return table == null ? null : _mapper.Map<TableResponse>(table);
        }
    }
}