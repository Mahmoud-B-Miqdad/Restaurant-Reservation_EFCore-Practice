using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Db.Repositories.Interfaces;
public interface IRestaurantRepository
{
    Task<List<Restaurant>> GetAllAsync();
    Task AddAsync(Restaurant restaurant);
    Task UpdateAsync(Restaurant restaurant);
    Task DeleteAsync(int id);
}
