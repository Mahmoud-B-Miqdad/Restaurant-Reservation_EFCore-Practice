using RestaurantReservation.Db.Models;
using RestaurantReservation.Services;

public class OrderService
{
    private readonly OrderOperations _orderOperations;

    public OrderService(OrderOperations orderOperations)
    {
        _orderOperations = orderOperations;
    }

    public async Task AddOrderAsync()
    {
        var order = new Order
        {
            ReservationId = 1, 
            EmployeeId = 1,
            OrderDate = DateTime.Now,
            TotalAmount = 50.00m
        };

        try
        {
            await _orderOperations.AddAsync(order);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Failed to add the order.", ex);
        }
    }

    public async Task GetAllOrdersAsync()
    {
        try
        {
            var allOrders = await _orderOperations.GetAllAsync();
            foreach (var order in allOrders)
            {
                Console.WriteLine($"[Order] ID: {order.OrderId}, ReservationId: {order.ReservationId}, EmployeeId: {order.EmployeeId}, Total: {order.TotalAmount}");
            }
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Failed to retrieve orders from the database.", ex);
        }
    }

    public async Task UpdateOrderAsync()
    {
        var updatedOrder = new Order
        {
            OrderId = 1,
            ReservationId = 1,
            EmployeeId = 1,
            OrderDate = DateTime.Now,
            TotalAmount = 65.00m
        };

        try
        {
            await _orderOperations.UpdateAsync(updatedOrder);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Failed to update the order.", ex);
        }
    }

    public async Task DeleteOrderAsync()
    {
        int orderIdToDelete = 1;

        try
        {
            await _orderOperations.DeleteAsync(orderIdToDelete);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Failed to delete the order.", ex);
        }
    }

    public async Task ExecuteExamplesAsync()
    {
        await AddOrderAsync();
        await UpdateOrderAsync();
        await GetAllOrdersAsync();
        await DeleteOrderAsync();
    }
}
