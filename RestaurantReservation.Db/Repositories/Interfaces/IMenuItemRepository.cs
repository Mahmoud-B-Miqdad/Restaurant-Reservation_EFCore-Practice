using RestaurantReservation.Db.Entities;

namespace RestaurantReservation.Db.Repositories.Interfaces;

public interface IMenuItemRepository
{
    Task<List<MenuItem>> GetAllAsync();
    Task<MenuItem> GetByIdAsync(int id);
    Task AddAsync(MenuItem menuItem);
    Task UpdateAsync(MenuItem menuItem);
    Task DeleteAsync(int id);
    Task<IEnumerable<MenuItem>> GetByRestaurantIdAsync(int restaurantId);
    Task<Restaurant?> GetRestaurantByMenuItemIdAsync(int menuItemId);
}
