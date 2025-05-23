using RestaurantReservation.Db.Entities;

namespace RestaurantReservation.Db.Repositories.Interfaces;

public interface IOrderRepository
{
    Task<List<Order>> GetAllAsync();
    Task<Order> GetByIdAsync(int id);
    Task AddAsync(Order order);
    Task UpdateAsync(Order order);
    Task DeleteAsync(int id);
    Task<IEnumerable<Order>> GetOrdersByEmployeeIdAsync(int employeeId);
    Task<Employee?> GetEmployeeByOrderIdAsync(int orderId);
    Task<Reservation?> GetReservationByOrderIdAsync(int orderId);

}