using RestaurantReservationSystem.API.DTOs.Requests;
using RestaurantReservationSystem.API.DTOs.Responses;

namespace RestaurantReservationSystem.API.Services.Interfaces
{
    /// <summary>
    /// Defines the contract for restaurant-related operations.
    /// </summary>
    public interface IRestaurantService
    {
        /// <summary>
        /// Retrieves all restaurants.
        /// </summary>
        /// <returns>A collection of restaurant response DTOs.</returns>
        Task<IEnumerable<RestaurantResponse>> GetAllAsync();

        /// <summary>
        /// Retrieves a restaurant by its ID.
        /// </summary>
        /// <param name="id">The ID of the restaurant.</param>
        /// <returns>The restaurant response DTO.</returns>
        Task<RestaurantResponse> GetByIdAsync(int id);

        /// <summary>
        /// Creates a new restaurant.
        /// </summary>
        /// <param name="request">The restaurant request DTO containing creation data.</param>
        /// <returns>The newly created restaurant response DTO.</returns>
        Task<RestaurantResponse> CreateAsync(RestaurantRequest request);

        /// <summary>
        /// Updates an existing restaurant.
        /// </summary>
        /// <param name="id">The ID of the restaurant to update.</param>
        /// <param name="request">The restaurant request DTO with updated data.</param>
        /// <returns>The updated restaurant response DTO, or null if not found.</returns>
        Task<RestaurantResponse?> UpdateAsync(int id, RestaurantRequest request);

        /// <summary>
        /// Deletes a restaurant by its ID.
        /// </summary>
        /// <param name="id">The ID of the restaurant to delete.</param>
        /// <returns>True if the restaurant was deleted successfully; otherwise, false.</returns>
        Task<bool> DeleteAsync(int id);

        /// <summary>
        /// Retrieves a list of employees who work at the specified restaurant.
        /// </summary>
        /// <param name="restaurantId">The ID of the restaurant to get employees for.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a collection of employee response DTOs.</returns>
        Task<IEnumerable<EmployeeResponse>> GetEmployeesAsync(int restaurantId);

        /// <summary>
        /// Retrieves a collection of tables associated with a specific restaurant.
        /// </summary>
        /// <param name="restaurantId">The unique identifier of the restaurant.</param>
        /// <returns>
        /// A task representing the asynchronous operation. The task result contains
        /// an enumerable collection of <see cref="TableResponse"/> objects representing the tables.
        /// </returns>
        Task<IEnumerable<TableResponse>> GetTablesAsync(int restaurantId);

        /// <summary>
        /// Retrieves all menu items available at a specific restaurant.
        /// </summary>
        /// <param name="restaurantId">The unique identifier of the restaurant whose menu items are to be retrieved.</param>
        /// <returns>
        /// A task that represents the asynchronous operation. The task result contains a collection
        /// of <see cref="MenuItemResponse"/> representing the menu items of the specified restaurant.
        /// </returns>
        Task<IEnumerable<MenuItemResponse>> GetMenuItemsAsync(int restaurantId);

        /// <summary>
        /// Retrieves all reservations made for a specific restaurant.
        /// </summary>
        /// <param name="restaurantId">The unique identifier of the restaurant.</param>
        /// <returns>
        /// A task that represents the asynchronous operation. The task result contains a collection of 
        /// <see cref="ReservationResponse"/> objects representing the reservations for the specified restaurant.
        /// </returns>
        Task<IEnumerable<ReservationResponse>> GetReservationsAsync(int restaurantId);

    }
}