using RestaurantReservation.Db.Entities;

namespace RestaurantReservation.Db.Repositories.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<List<Employee>> GetAllAsync();
        Task<Employee> GetByIdAsync(int id);
        Task AddAsync(Employee employee);
        Task UpdateAsync(Employee employee);
        Task DeleteAsync(int id);
        Task<IEnumerable<Employee>> GetByRestaurantIdAsync(int restaurantId);
        Task<IEnumerable<Order>> GetOrdersByEmployeeIdAsync(int employeeId);
        Task<Restaurant?> GetRestaurantByEmployeeIdAsync(int employeeId);
    }
}