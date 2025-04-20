using RestaurantReservation.Db.Models;
using RestaurantReservation.Services;

public class OrderItemService
{
    private readonly OrderItemOperations _orderItemOperations;

    public OrderItemService(OrderItemOperations orderItemOperations)
    {
        _orderItemOperations = orderItemOperations;
    }

    public async Task AddOrderItemAsync()
    {
        var orderItem = new OrderItem
        {
            OrderId = 1,     
            ItemId = 1,
            Quantity = 2
        };

        try
        {
            await _orderItemOperations.AddAsync(orderItem);
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
            var allOrderItems = await _orderItemOperations.GetAllAsync();
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

    public async Task UpdateOrderItemAsync()
    {
        var updatedOrderItem = new OrderItem
        {
            OrderItemId = 1, 
            OrderId = 1,
            ItemId = 1,
            Quantity = 3
        };

        try
        {
            await _orderItemOperations.UpdateAsync(updatedOrderItem);
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
            await _orderItemOperations.DeleteAsync(itemIdToDelete);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Failed to delete the order item.", ex);
        }
    }

    public async Task ExecuteExamplesAsync()
    {
        await AddOrderItemAsync();
        await UpdateOrderItemAsync();
        await GetAllOrderItemsAsync();
        await DeleteOrderItemAsync();
    }
}
