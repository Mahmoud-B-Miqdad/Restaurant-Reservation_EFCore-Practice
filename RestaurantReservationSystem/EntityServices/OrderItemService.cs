using Microsoft.EntityFrameworkCore;
using RestaurantReservation.Db.Models;
using RestaurantReservation.Db.Repositories.Interfaces;
using RestaurantReservation.Db.Services.Interfaces;
using RestaurantReservationSystem.Constants;

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
            throw new InvalidOperationException(DefaultErrorMessages.AddFailed, ex);
        }
    }

    public async Task<IEnumerable<OrderItem>> GetAllOrderItemsAsync()
    {
        try
        {
            var allOrderItems = await _orderItemRepository.GetAllAsync();
            return allOrderItems;
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException(DefaultErrorMessages.RetrieveFailed, ex);
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
            throw new InvalidOperationException(DefaultErrorMessages.UpdateFailed, ex);
        }
    }

    public async Task DeleteOrderItemAsync(int itemIdToDelete)
    {

        try
        {
            await _orderItemRepository.DeleteAsync(itemIdToDelete);
        }
        catch (DbUpdateException ex)
        {
            throw new InvalidOperationException(DefaultErrorMessages.DeleteWithRelations, ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException(DefaultErrorMessages.DeleteUnexpected, ex);
        }
    }

    public async Task ExecuteExamplesAsync()
    {
        await AddOrderItemAsync(
            orderId: DefaultTestValues.Id1,
            itemId: DefaultTestValues.Id1,
            quantity: DefaultTestValues.DefaultQuantity);

        await UpdateOrderItemAsync(
            orderItemId: DefaultTestValues.Id1,
            UpdatedorderId: DefaultTestValues.Id2,
            UpdateditemId: DefaultTestValues.Id4,
            Updatedquantity: DefaultTestValues.UpdatedQuantity);

        var orderItems = await GetAllOrderItemsAsync();
        foreach (var item in orderItems)
        {
            Console.WriteLine($"[OrderItem] ID: {item.OrderItemId}, OrderId: {item.OrderId}, ItemId: {item.ItemId}, Quantity: {item.Quantity}");
        }

        await DeleteOrderItemAsync(2);
    }
}
