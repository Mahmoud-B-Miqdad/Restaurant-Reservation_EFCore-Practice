namespace RestaurantReservation.Db.Services.Interfaces
{
    public interface IOrderService
    {
        Task AddOrderAsync();
        Task UpdateOrderAsync();
        Task GetAllOrdersAsync();
        Task DeleteOrderAsync();
        Task ExecuteExamplesAsync();
    }
}