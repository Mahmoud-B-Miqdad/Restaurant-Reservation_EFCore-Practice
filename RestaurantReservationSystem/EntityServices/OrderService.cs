using RestaurantReservation.Db.Models;
using RestaurantReservation.Db.Repositories;
public class OrderService
{
    private readonly OrderRepository _orderRepository;

    public OrderService(OrderRepository orderRepository)
    {
        this._orderRepository = orderRepository;
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
            await _orderRepository.AddAsync(order);
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
            var allOrders = await _orderRepository.GetAllAsync();
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
            await _orderRepository.UpdateAsync(updatedOrder);
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
            await _orderRepository.DeleteAsync(orderIdToDelete);
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
