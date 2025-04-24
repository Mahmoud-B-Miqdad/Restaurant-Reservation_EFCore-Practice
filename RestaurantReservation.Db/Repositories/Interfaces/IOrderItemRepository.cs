using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Db.Repositories.Interfaces;

public interface IOrderItemRepository
{
    Task<List<OrderItem>> GetAllAsync();
    Task AddAsync(OrderItem orderItem);
    Task UpdateAsync(OrderItem orderItem);
    Task DeleteAsync(int id);
}
