using Microsoft.EntityFrameworkCore;
using RestaurantReservation.Db.Models;
using RestaurantReservation.Db.Repositories;
using RestaurantReservation.Db.Repositories.Interfaces;
using RestaurantReservation.Db.Services.Interfaces;
using RestaurantReservationSystem.Constants;
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
            throw new InvalidOperationException(DefaultErrorMessages.AddFailed, ex);
        }
    }

    public async Task<IEnumerable<Order>> GetAllOrdersAsync()
    {
        try
        {
            var allOrders = await _orderRepository.GetAllAsync();
            return allOrders;
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException(DefaultErrorMessages.RetrieveFailed, ex);
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
            throw new InvalidOperationException(DefaultErrorMessages.UpdateFailed, ex);
        }
    }

    public async Task DeleteOrderAsync(int orderIdToDelete)
    {

        try
        {
            await _orderRepository.DeleteAsync(orderIdToDelete);
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
        await AddOrderAsync(
            reservationId: DefaultTestValues.Id1,
            employeeId: DefaultTestValues.Id1,
            orderDate: DefaultTestValues.CurrentDate,
            totalAmount: DefaultTestValues.DefaultTotalAmount);

        await UpdateOrderAsync(
            orderId: DefaultTestValues.Id1,
            UpdatedreservationId: DefaultTestValues.Id1,
            UpdatedemployeeId: DefaultTestValues.Id1,
            UpdatedOrderDate: DefaultTestValues.CurrentDate,
            UpdatedTotalAmount: DefaultTestValues.UpdatedTotalAmount);

        var orders = await GetAllOrdersAsync();
        foreach (var order in orders)
        {
            Console.WriteLine($"[Order] ID: {order.OrderId}, ReservationId: {order.ReservationId}, EmployeeId: {order.EmployeeId}, Total: {order.TotalAmount}");
        }

        await DeleteOrderAsync(1);
    }
}
