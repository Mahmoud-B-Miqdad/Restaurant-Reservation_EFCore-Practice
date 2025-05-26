using RestaurantReservationSystem.Domain.Models;

namespace RestaurantReservationSystem.Domain.Interfaces.Repositories;

public interface IMenuItemRepository
{
    Task<List<MenuItemModel>> GetAllAsync();
    Task<MenuItemModel> GetByIdAsync(int id);
    Task AddAsync(MenuItemModel menuItem);
    Task UpdateAsync(MenuItemModel menuItem);
    Task DeleteAsync(int id);
    Task<IEnumerable<MenuItemModel>> GetByRestaurantIdAsync(int restaurantId);
}
