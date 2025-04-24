namespace RestaurantReservation.Db.Services.Interfaces
{
    public interface IOrderService
    {
        Task AddOrderAsync(int reservationId, int employeeId, DateTime orderDate, decimal totalAmount);
        Task UpdateOrderAsync(int UpdatedorderId, int UpdatedreservationId,
        int UpdatedemployeeId, DateTime UpdatedOrderDate, decimal UpdatedTotalAmount);
        Task GetAllOrdersAsync();
        Task DeleteOrderAsync();
        Task ExecuteExamplesAsync();
    }
}