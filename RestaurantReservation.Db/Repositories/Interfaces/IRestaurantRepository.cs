using RestaurantReservation.Db.Entities;

namespace RestaurantReservation.Db.Repositories.Interfaces;
public interface IRestaurantRepository
{
    Task<List<Restaurant>> GetAllAsync();
    Task<Restaurant> GetByIdAsync(int id);
    Task AddAsync(Restaurant restaurant);
    Task UpdateAsync(Restaurant restaurant);
    Task DeleteAsync(int id);
}
