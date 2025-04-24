using RestaurantReservation.Db.Models;
using RestaurantReservation.Db.Repositories;
using RestaurantReservation.Db.Repositories.Interfaces;
using RestaurantReservation.Db.Services.Interfaces;

public class OrderItemService : IOrderItemService
{
    private readonly IOrderItemRepository _orderItemRepository;

    public OrderItemService(IOrderItemRepository orderItemRepository)
    {
        _orderItemRepository = orderItemRepository;
    }

    public async Task AddOrderItemAsync(int orderId,int itemId, int quantity)
    {
        var orderItem = new OrderItem
        {
            OrderId = orderId,     
            ItemId = itemId,
            Quantity = quantity
        };

        try
        {
            await _orderItemRepository.AddAsync(orderItem);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Failed to add the order item.", ex);
        }
    }

    public async Task GetAllOrderItemsAsync()
    {
        try
        {
            var allOrderItems = await _orderItemRepository.GetAllAsync();
            foreach (var item in allOrderItems)
            {
                Console.WriteLine($"[OrderItem] ID: {item.OrderItemId}, OrderId: {item.OrderId}, ItemId: {item.ItemId}, Quantity: {item.Quantity}");
            }
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Failed to retrieve order items from the database.", ex);
        }
    }

    public async Task UpdateOrderItemAsync(int orderItemId, int UpdatedorderId, int UpdateditemId, 
        int Updatedquantity)
    {
        var updatedOrderItem = new OrderItem
        {
            OrderItemId = orderItemId, 
            OrderId = UpdatedorderId,
            ItemId = UpdateditemId,
            Quantity = Updatedquantity
        };

        try
        {
            await _orderItemRepository.UpdateAsync(updatedOrderItem);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Failed to update the order item.", ex);
        }
    }

    public async Task DeleteOrderItemAsync()
    {
        int itemIdToDelete = 1; 

        try
        {
            await _orderItemRepository.DeleteAsync(itemIdToDelete);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Failed to delete the order item.", ex);
        }
    }

    public async Task ExecuteExamplesAsync()
    {
        await AddOrderItemAsync(
            orderId: 1,
            itemId: 1,
            quantity: 20);

        await UpdateOrderItemAsync(
            orderItemId: 1,
            UpdatedorderId: 2,
            UpdateditemId: 4,
            Updatedquantity: 30);

        await GetAllOrderItemsAsync();
        await DeleteOrderItemAsync();
    }
}
