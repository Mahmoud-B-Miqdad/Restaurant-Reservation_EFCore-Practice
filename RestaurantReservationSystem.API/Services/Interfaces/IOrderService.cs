using RestaurantReservation.Db.Entities;
using RestaurantReservationSystem.API.DTOs.Requests;
using RestaurantReservationSystem.API.DTOs.Responses;

namespace RestaurantReservationSystem.API.Services.Interfaces
{
    /// <summary>
    /// Defines the contract for order-related operations within the Restaurant Reservation System.
    /// </summary>
    public interface IOrderService
    {
        /// <summary>
        /// Retrieves all orders in the system.
        /// </summary>
        /// <returns>A list of <see cref="OrderResponse"/> representing all orders.</returns>
        Task<List<OrderResponse>> GetAllAsync();

        /// <summary>
        /// Retrieves a specific order by its unique identifier.
        /// </summary>
        /// <param name="id">The ID of the order.</param>
        /// <returns>The corresponding <see cref="OrderResponse"/> if found; otherwise, null.</returns>
        Task<OrderResponse?> GetByIdAsync(int id);

        /// <summary>
        /// Creates a new order in the system.
        /// </summary>
        /// <param name="request">The order details provided in <see cref="OrderRequest"/> format.</param>
        Task<OrderItemResponse?> CreateAsync(OrderRequest request);

        /// <summary>
        /// Updates an existing order by its ID.
        /// </summary>
        /// <param name="id">The ID of the order to update.</param>
        /// <param name="request">The updated order data.</param>
        Task<OrderItemResponse?> UpdateAsync(int id, OrderRequest request);

        /// <summary>
        /// Deletes an order by its unique identifier.
        /// </summary>
        /// <param name="id">The ID of the order to delete.</param>
        Task<bool> DeleteAsync(int id);

        /// <summary>
        /// Retrieves the reservation associated with a given order.
        /// </summary>
        /// <param name="orderId">The ID of the order.</param>
        /// <returns>The associated <see cref="ReservationResponse"/> if found; otherwise, null.</returns>
        Task<ReservationResponse?> GetReservationAsync(int orderId);

        /// <summary>
        /// Retrieves the employee who processed a given order.
        /// </summary>
        /// <param name="orderId">The ID of the order.</param>
        /// <returns>The associated <see cref="EmployeeResponse"/> if found; otherwise, null.</returns>
        Task<EmployeeResponse?> GetEmployeeAsync(int orderId);

        /// <summary>
        /// Retrieves all order items handled by a specific employee.
        /// </summary>
        /// <param name="employeeId">The ID of the employee.</param>
        /// <returns>A collection of <see cref="OrderItemResponse"/> linked to the employee.</returns>
        Task<IEnumerable<OrderItemResponse>> GetOrderItemsAsync(int orderId);
    }
}