using RestaurantReservation.Db.Entities;

namespace RestaurantReservationSystem.Domain.Interfaces.Repositories;

public interface IOrderItemRepository
{
    Task<List<OrderItem>> GetAllAsync();
    Task<OrderItem> GetByIdAsync(int id);
    Task AddAsync(OrderItem orderItem);
    Task UpdateAsync(OrderItem orderItem);
    Task DeleteAsync(int id);
}
