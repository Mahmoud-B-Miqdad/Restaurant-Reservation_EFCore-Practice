using RestaurantReservation.Db.Entities;

namespace RestaurantReservationSystem.Domain.Interfaces.Repositories;

public interface IMenuItemRepository
{
    Task<List<MenuItem>> GetAllAsync();
    Task<MenuItem> GetByIdAsync(int id);
    Task AddAsync(MenuItem menuItem);
    Task UpdateAsync(MenuItem menuItem);
    Task DeleteAsync(int id);
    Task<IEnumerable<MenuItem>> GetByRestaurantIdAsync(int restaurantId);
}
