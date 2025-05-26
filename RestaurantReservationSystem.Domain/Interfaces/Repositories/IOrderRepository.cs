using RestaurantReservationSystem.Domain.Models;

namespace RestaurantReservationSystem.Domain.Interfaces.Repositories;

public interface IOrderRepository
{
    Task<List<OrderModel>> GetAllAsync();
    Task<OrderModel> GetByIdAsync(int id);
    Task AddAsync(OrderModel order);
    Task UpdateAsync(OrderModel order);
    Task DeleteAsync(int id);
    Task<IEnumerable<OrderModel>> GetOrdersByEmployeeIdAsync(int employeeId);
}
