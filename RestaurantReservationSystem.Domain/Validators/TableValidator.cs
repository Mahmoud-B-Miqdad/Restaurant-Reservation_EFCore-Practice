using RestaurantReservationSystem.Domain.Exceptions;
using RestaurantReservationSystem.Domain.Interfaces.Repositories;
using RestaurantReservationSystem.Domain.Models;

namespace RestaurantReservationSystem.Domain.Validators
{
    public class TableValidator
    {
        private readonly ITableRepository _tableRepository;

        public TableValidator(ITableRepository tableRepository)
        {
            _tableRepository = tableRepository;
        }

        public async Task<TableModel> EnsureTableExistsAsync(int tableId)
        {
            var table = await _tableRepository.GetByIdAsync(tableId);
            if (table == null)
                throw new NotFoundException($"Table with ID {tableId} not found");

            return table;
        }
    }

}