using RestaurantReservation.Db.Entities;

namespace RestaurantReservation.Db.Services.Interfaces
{
    public interface IOrderItemService
    {
        Task AddOrderItemAsync(int orderId, int itemId, int quantity);
        Task UpdateOrderItemAsync(int UpdatedorderItemId, int UpdatedorderId, int UpdateditemId,
        int Updatedquantity);
        Task<IEnumerable<OrderItem>> GetAllOrderItemsAsync();
        Task DeleteOrderItemAsync(int orderItemIdToDelete);
        Task ExecuteExamplesAsync();
    }
}