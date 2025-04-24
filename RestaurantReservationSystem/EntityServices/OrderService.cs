using RestaurantReservation.Db.Models;
using RestaurantReservation.Db.Repositories;
using RestaurantReservation.Db.Repositories.Interfaces;
using RestaurantReservation.Db.Services.Interfaces;
public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;

    public OrderService(IOrderRepository orderRepository)
    {
        this._orderRepository = orderRepository;
    }

    public async Task AddOrderAsync(int reservationId, int employeeId, DateTime orderDate, decimal totalAmount)
    {
        var order = new Order
        {
            ReservationId = reservationId,
            EmployeeId = employeeId,
            OrderDate = orderDate,
            TotalAmount = totalAmount
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

    public async Task UpdateOrderAsync(int orderId, int UpdatedreservationId,
        int UpdatedemployeeId, DateTime UpdatedOrderDate, decimal UpdatedTotalAmount)
    {
        var updatedOrder = new Order
        {
            OrderId = orderId,
            ReservationId = UpdatedreservationId,
            EmployeeId = UpdatedemployeeId,
            OrderDate = UpdatedOrderDate,
            TotalAmount = UpdatedTotalAmount
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
        await AddOrderAsync(
            reservationId: 1,
            employeeId: 1,
            orderDate: DateTime.Now,
            totalAmount: 50.00m);

        await UpdateOrderAsync(
            orderId: 1,
            UpdatedreservationId: 1,
            UpdatedemployeeId: 1,
            UpdatedOrderDate: DateTime.Now,
            UpdatedTotalAmount: 65.00m
        );

        await GetAllOrdersAsync();
        await DeleteOrderAsync();
    }
}
