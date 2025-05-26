using RestaurantReservation.Db.Entities;

namespace RestaurantReservationSystem.Domain.Interfaces.Repositories;

public interface IOrderRepository
{
    Task<List<Order>> GetAllAsync();
    Task<Order> GetByIdAsync(int id);
    Task AddAsync(Order order);
    Task UpdateAsync(Order order);
    Task DeleteAsync(int id);
    Task<IEnumerable<Order>> GetOrdersByEmployeeIdAsync(int employeeId);
}
