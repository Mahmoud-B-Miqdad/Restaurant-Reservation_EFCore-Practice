namespace RestaurantReservation.Db.Services.Interfaces
{
    public interface IOrderItemService
    {
        Task AddOrderItemAsync();
        Task UpdateOrderItemAsync();
        Task GetAllOrderItemsAsync();
        Task DeleteOrderItemAsync();
        Task ExecuteExamplesAsync();
    }
}