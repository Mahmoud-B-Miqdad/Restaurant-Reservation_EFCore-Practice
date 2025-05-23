using RestaurantReservation.Db.Entities;
using RestaurantReservationSystem.API.DTOs.Requests;
using RestaurantReservationSystem.API.DTOs.Responses;

namespace RestaurantReservationSystem.API.Services.Interfaces
{
    /// <summary>
    /// Defines the contract for managing reservation-related operations.
    /// </summary>
    public interface IReservationService
    {
        /// <summary>
        /// Retrieves a list of all reservation.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains a list of reservation response DTOs.</returns>
        Task<List<ReservationResponse>> GetAllAsync();

        /// <summary>
        /// Retrieves a specific reservation by their unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the reservation.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the reservation response DTO if found; otherwise, null.</returns>
        Task<ReservationResponse?> GetByIdAsync(int id);

        /// <summary>
        /// Adds a new reservation to the system.
        /// </summary>
        /// <param name="request">The request DTO containing reservation details.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task<ReservationResponse> CreateAsync(ReservationRequest request);

        /// <summary>
        /// Updates the details of an existing reservation.
        /// </summary>
        /// <param name="id">The unique identifier of the reservation to update.</param>
        /// <param name="request">The request DTO containing updated reservation information.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task<ReservationResponse?> UpdateAsync(int id, ReservationRequest request);

        /// <summary>
        /// Deletes an reservation from the system.
        /// </summary>
        /// <param name="id">The unique identifier of the reservation to delete.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task<bool> DeleteAsync(int id);

        /// <summary>
        /// Retrieves the customer associated with a specific reservation.
        /// </summary>
        /// <param name="reservationId">The unique identifier of the reservation.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the customer response DTO if found; otherwise, null.</returns>
        Task<CustomerResponse?> GetCustomerAsync(int reservationId);

        /// <summary>
        /// Retrieves the restaurant associated with a specific reservation.
        /// </summary>
        /// <param name="reservationId">The unique identifier of the reservation.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the restaurant response DTO if found; otherwise, null.</returns>
        Task<RestaurantResponse?> GetRestaurantAsync(int reservationId);

        /// <summary>
        /// Retrieves the table associated with a specific reservation.
        /// </summary>
        /// <param name="reservationId">The unique identifier of the reservation.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the table response DTO if found; otherwise, null.</returns>
        Task<TableResponse?> GetTableAsync(int reservationId);

        /// <summary>
        /// Retrieves all orders handled by a specific reservation.
        /// </summary>
        /// <param name="reservationId">The unique identifier of the reservation.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a list of order response DTOs.</returns>
        Task<IEnumerable<OrderResponse>> GetOrdersAsync(int reservationId);

        /// <summary>
        /// Retrieves a list of all Reservation handled by a specific Customer.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation.
        /// The task result contains a list of Reservation response DTOs.
        /// </returns>
        Task<IEnumerable<ReservationResponse>> GetReservationsByCustomerAsync(int customerId);

        /// <summary>
        /// Retrieves all Ordered Menu Items handled by a specific reservation.
        /// </summary>
        /// <param name="reservationId">The unique identifier of the reservation</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a list of menuItem response DTOs.</returns>
        Task<IEnumerable<MenuItemResponse>> GetOrderedMenuItemsAsync(int reservationId);
    }
}