using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Db.Services.Interfaces
{
    public interface IOrderService
    {
        Task AddOrderAsync(int reservationId, int employeeId, DateTime orderDate, decimal totalAmount);
        Task UpdateOrderAsync(int UpdatedorderId, int UpdatedreservationId,
        int UpdatedemployeeId, DateTime UpdatedOrderDate, decimal UpdatedTotalAmount);
        Task<IEnumerable<Order>> GetAllOrdersAsync();
        Task DeleteOrderAsync(int orderIdToDelete);
        Task ExecuteExamplesAsync();
    }
}