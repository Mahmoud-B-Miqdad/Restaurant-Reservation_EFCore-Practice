namespace RestaurantReservation.Db.Services.Interfaces
{
    public interface IOrderItemService
    {
        Task AddOrderItemAsync(int orderId, int itemId, int quantity);
        Task UpdateOrderItemAsync(int UpdatedorderItemId, int UpdatedorderId, int UpdateditemId,
        int Updatedquantity);
        Task GetAllOrderItemsAsync();
        Task DeleteOrderItemAsync();
        Task ExecuteExamplesAsync();
    }
}