using RestaurantReservation.Db.Models;
using RestaurantReservation.Db.Repositories;

public class OrderItemService
{
    private readonly OrderItemRepository _orderItemRepository;

    public OrderItemService(OrderItemRepository orderItemRepository)
    {
        _orderItemRepository = orderItemRepository;
    }

    public async Task AddOrderItemAsync()
    {
        var orderItem = new OrderItem
        {
            OrderId = 1,     
            ItemId = 1,
            Quantity = 20
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

    public async Task UpdateOrderItemAsync()
    {
        var updatedOrderItem = new OrderItem
        {
            OrderItemId = 1, 
            OrderId = 1,
            ItemId = 1,
            Quantity = 30
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
        //await AddOrderItemAsync();
        await UpdateOrderItemAsync();
        //await GetAllOrderItemsAsync();
        //await DeleteOrderItemAsync();
    }
}
