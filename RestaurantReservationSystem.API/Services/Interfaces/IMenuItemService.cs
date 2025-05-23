using RestaurantReservationSystem.API.DTOs.Requests;
using RestaurantReservationSystem.API.DTOs.Responses;

namespace RestaurantReservationSystem.API.Services.Interfaces
{
    /// <summary>
    /// Defines the contract for managing menuItem-related operations.
    /// </summary>
    public interface IMenuItemService
    {
        /// <summary>
        /// Retrieves a list of all menuItem.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains a list of menuItem response DTOs.</returns>
        Task<List<MenuItemResponse>> GetAllAsync();

        /// <summary>
        /// Retrieves a specific menuItem by their unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the menuItem.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the menuItem response DTO if found; otherwise, null.</returns>
        Task<MenuItemResponse?> GetByIdAsync(int id);

        /// <summary>
        /// Adds a new menuItem to the system.
        /// </summary>
        /// <param name="request">The request DTO containing menuItem details.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task<MenuItemResponse> CreateAsync(EmployeeRequest request);

        /// <summary>
        /// Updates the details of an existing menuItem.
        /// </summary>
        /// <param name="id">The unique identifier of the menuItem to update.</param>
        /// <param name="request">The request DTO containing updated menuItem information.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task<MenuItemResponse?> UpdateAsync(int id, EmployeeRequest request);

        /// <summary>
        /// Deletes an menuItem from the system.
        /// </summary>
        /// <param name="id">The unique identifier of the menuItem to delete.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task<bool> DeleteAsync(int id);

        /// <summary>
        /// Retrieves all orderItems handled by a specific orderItems.
        /// </summary>
        /// <param name="menuItemId">The unique identifier of the menuItem</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a list of orderitem response DTOs.</returns>
        Task<IEnumerable<OrderItemResponse>> GetOrderItemsAsync(int menuItemId);

        /// <summary>
        /// Retrieves the restaurant associated with a specific menuItem.
        /// </summary>
        /// <param name="menuItemId">The unique identifier of the menuItem.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the restaurant response DTO if found; otherwise, null.</returns>
        Task<RestaurantResponse?> GetRestaurantAsync(int menuItemId);
    }
}