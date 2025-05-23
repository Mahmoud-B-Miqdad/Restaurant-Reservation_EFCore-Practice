using RestaurantReservation.Db.Entities;

namespace RestaurantReservation.Db.Repositories.Interfaces;

public interface IOrderItemRepository
{
    Task<List<OrderItem>> GetAllAsync();
    Task<OrderItem> GetByIdAsync(int id);
    Task AddAsync(OrderItem orderItem);
    Task UpdateAsync(OrderItem orderItem);
    Task DeleteAsync(int id);
    Task<IEnumerable<OrderItem>> GetOrderItemsByOrderIdAsync(int orderId);
    Task<IEnumerable<OrderItem>> GetOrderItemsByMenuItemIdAsync(int menuItemId);
}
