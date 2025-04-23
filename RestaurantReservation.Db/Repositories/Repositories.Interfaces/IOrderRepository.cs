using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Db.Repositories.Interfaces;

internal interface IOrderRepository
{
    Task<List<Order>> GetAllAsync();
    Task AddAsync(Order order);
    Task UpdateAsync(Order order);
    Task DeleteAsync(int id);
}
