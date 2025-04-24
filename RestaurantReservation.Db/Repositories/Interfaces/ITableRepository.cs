using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Db.Repositories.Interfaces;

public interface ITableRepository
{
    Task<List<Table>> GetAllAsync();
    Task AddAsync(Table table);
    Task UpdateAsync(Table table);
    Task DeleteAsync(int id);
}
