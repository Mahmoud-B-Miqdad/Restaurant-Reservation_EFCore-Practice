using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Db.Repositories.Interfaces;

internal interface IMenuItemRepository
{
    Task<List<MenuItem>> GetAllAsync();
    Task AddAsync(MenuItem menuItem);
    Task UpdateAsync(MenuItem menuItem);
    Task DeleteAsync(int id);
}
