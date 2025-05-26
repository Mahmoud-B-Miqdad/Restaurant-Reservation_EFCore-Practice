using RestaurantReservation.Db.Entities;

namespace RestaurantReservationSystem.Domain.Interfaces.Repositories;

public interface ITableRepository
{
    Task<List<Table>> GetAllAsync();
    Task<Table> GetByIdAsync(int id);
    Task AddAsync(Table table);
    Task UpdateAsync(Table table);
    Task DeleteAsync(int id);
    Task<IEnumerable<Table>> GetByRestaurantIdAsync(int restaurantId);
}
