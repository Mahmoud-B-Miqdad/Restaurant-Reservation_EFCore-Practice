using RestaurantReservationSystem.API.DTOs.Requests;
using RestaurantReservationSystem.API.DTOs.Responses;

namespace RestaurantReservationSystem.API.Services.Interfaces
{
    /// <summary>
    /// Defines the contract for managing orderItem-related operations.
    /// </summary>
    public interface IOrderItemService
    {
        /// <summary>
        /// Retrieves a list of all orderItem.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains a list of orderItem response DTOs.</returns>
        Task<List<OrderItemResponse>> GetAllAsync();

        /// <summary>
        /// Retrieves a specific orderItem by their unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the orderItem.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the orderItem response DTO if found; otherwise, null.</returns>
        Task<OrderItemResponse?> GetByIdAsync(int id);

        /// <summary>
        /// Adds a new orderItem to the system.
        /// </summary>
        /// <param name="request">The request DTO containing orderItem details.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task<OrderItemResponse> CreateAsync(OrderItemRequest request);

        /// <summary>
        /// Updates the details of an existing orderItem.
        /// </summary>
        /// <param name="id">The unique identifier of the orderItem to update.</param>
        /// <param name="request">The request DTO containing updated orderItem information.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task<OrderItemResponse?> UpdateAsync(int id, OrderItemRequest request);

        /// <summary>
        /// Deletes an orderItem from the system.
        /// </summary>
        /// <param name="id">The unique identifier of the orderItem to delete.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task<bool> DeleteAsync(int id);

        /// <summary>
        /// Retrieves the restaurant associated with a specific order.
        /// </summary>
        /// <param name="orderItemId">The unique identifier of the order.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the restaurant response DTO if found; otherwise, null.</returns>
        Task<OrderItemResponse?> GetOrderAsync(int orderItemId);

        /// <summary>
        /// Retrieves the restaurant associated with a specific order.
        /// </summary>
        /// <param name="orderItemId">The unique identifier of the order.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the restaurant response DTO if found; otherwise, null.</returns>
        Task<MenuItemResponse?> GetMenuItemAsync(int orderItemId);
    }
}