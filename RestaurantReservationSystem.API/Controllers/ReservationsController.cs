using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using RestaurantReservationSystem.API.DTOs.Requests;
using RestaurantReservationSystem.API.DTOs.Responses;
using RestaurantReservationSystem.API.Responses;
using RestaurantReservationSystem.API.Services.Interfaces;

namespace RestaurantReservationSystem.API.Controllers
{
    /// <summary>
    /// Controller for managing reservation-related operations.
    /// </summary>
    [Route("api/reservations")]
    [ApiController]
    public class ReservationsController : ControllerBase
    {
        private readonly IReservationService _reservationService;

        public ReservationsController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        /// <summary>
        /// Retrieves all reservation.
        /// </summary>
        /// <returns>List of all reservation.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var reservations = await _reservationService.GetAllAsync();
            return Ok(ApiResponse<IEnumerable<ReservationResponse>>.SuccessResponse(reservations));
        }

        /// <summary>
        /// Retrieves a specific reservation by ID.
        /// </summary>
        /// <param name="id">Reservation ID</param>
        /// <returns>The reservation details if found.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var reservation = await _reservationService.GetByIdAsync(id);
            if (reservation == null)
                return NotFound(ApiResponse<ReservationResponse>.FailResponse("Reservation not found"));

            return Ok(ApiResponse<ReservationResponse>.SuccessResponse(reservation));
        }

        /// <summary>
        /// Creates a new reservation.
        /// </summary>
        /// <param name="request">Reservation creation request payload</param>
        /// <returns>The created reservation with location header.</returns>
        [HttpPost]
        public async Task<IActionResult> CreateAsync(ReservationRequest request)
        {
            var createdReservation = await _reservationService.CreateAsync(request);
            CreatedAtAction(
                nameof(GetByIdAsync),
                new { id = createdReservation.ReservationId },
               createdReservation);
            return Ok(ApiResponse<ReservationResponse>.SuccessResponse(createdReservation));
        }

        /// <summary>
        /// Updates an existing reservation completely.
        /// </summary>
        /// <param name="id">Reservation ID</param>
        /// <param name="request">Updated reservation data</param>
        /// <returns>The updated reservation details.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, ReservationRequest request)
        {
            var updatedReservation = await _reservationService.UpdateAsync(id, request);
            if (updatedReservation == null)
                return NotFound(ApiResponse<string>.FailResponse("Reservation not found"));

            return Ok(ApiResponse<ReservationResponse>.SuccessResponse(updatedReservation));
        }

        /// <summary>
        /// Applies partial update to an reservation.
        /// </summary>
        /// <param name="id">Reservation ID</param>
        /// <param name="patchDoc">JSON Patch document</param>
        /// <returns>The updated reservation details.</returns>
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchAsync(int id, [FromBody] JsonPatchDocument<ReservationRequest> patchDoc)
        {
            if (patchDoc == null)
                return BadRequest(ApiResponse<string>.FailResponse("Patch document cannot be null"));

            var existingReservation = await _reservationService.GetByIdAsync(id);
            if (existingReservation == null)
                return NotFound(ApiResponse<string>.FailResponse("Reservation not found"));

            var reservationToPatch = new ReservationRequest
            {
                CustomerId = existingReservation.CustomerId,
               RestaurantId = existingReservation.RestaurantId,
               TableId = existingReservation.TableId,
               ReservationDate = existingReservation.ReservationDate,
               PartySize = existingReservation.PartySize
            };

            patchDoc.ApplyTo(reservationToPatch, ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ApiResponse<string>.FailResponse("Invalid patch document"));

            if (!TryValidateModel(reservationToPatch))
                return BadRequest(ModelState);

            var updatedReservation = await _reservationService.UpdateAsync(id, reservationToPatch);
            if (updatedReservation == null)
                return NotFound(ApiResponse<string>.FailResponse("Failed to update employee"));

            return Ok(ApiResponse<ReservationResponse>.SuccessResponse(updatedReservation));
        }

        /// <summary>
        /// Deletes an reservation by ID.
        /// </summary>
        /// <param name="id">Reservation ID</param>
        /// <returns>Success message if deleted.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var deletedReservation = await _reservationService.DeleteAsync(id);
            if (!deletedReservation)
                return NotFound(ApiResponse<string>.FailResponse("Reservation not found"));

            return Ok(ApiResponse<string>.SuccessResponse("Reservation deleted successfully"));
        }

        /// <summary>
        /// Retrieves all orders handled by the specified reservation.
        /// </summary>
        /// <param name="id">Reservation ID</param>
        /// <returns>List of orders assigned to the reservation.</returns>
        [HttpGet("{id}/orders")]
        public async Task<IActionResult> GetOrdersAsync(int id)
        {
            var reservation = await _reservationService.GetByIdAsync(id);
            if (reservation == null)
                return NotFound(ApiResponse<ReservationResponse>.FailResponse("Reservation not found"));

            var orders = await _reservationService.GetOrdersAsync(id);
            return Ok(ApiResponse<IEnumerable<OrderResponse>>.SuccessResponse(orders));
        }

        /// <summary>
        /// Retrieves the customer to which the reservation belongs.
        /// </summary>
        /// <param name="id">Reservation ID</param>
        /// <returns>Customer details of the reservation.</returns>
        [HttpGet("{id}/customer")]
        public async Task<IActionResult> GetCustomerAsync(int id)
        {
            var employee = await _reservationService.GetByIdAsync(id);
            if (employee == null)
                return NotFound(ApiResponse<ReservationResponse>.FailResponse("Reservation not found"));

            var customer = await _reservationService.GetCustomerAsync(id);
            if (customer == null)
                return NotFound(ApiResponse<CustomerResponse>.FailResponse("Customer not found"));

            return Ok(ApiResponse<CustomerResponse>.SuccessResponse(customer));
        }

        /// <summary>
        /// Retrieves the restaurant to which the reservation belongs.
        /// </summary>
        /// <param name="id">Reservation ID</param>
        /// <returns>Restaurant details of the reservation.</returns>
        [HttpGet("{id}/restaurant")]
        public async Task<IActionResult> GetRestaurantAsync(int id)
        {
            var reservation = await _reservationService.GetByIdAsync(id);
            if (reservation == null)
                return NotFound(ApiResponse<ReservationResponse>.FailResponse("Reservation not found"));

            var restaurant = await _reservationService.GetRestaurantAsync(id);
            if (restaurant == null)
                return NotFound(ApiResponse<RestaurantResponse>.FailResponse("Restaurant not found"));

            return Ok(ApiResponse<RestaurantResponse>.SuccessResponse(restaurant));
        }

        /// <summary>
        /// Retrieves the table to which the reservation belongs.
        /// </summary>
        /// <param name="id">Reservation ID</param>
        /// <returns>Table details of the reservation.</returns>
        [HttpGet("{id}/table")]
        public async Task<IActionResult> GetTableAsync(int id)
        {
            var reservation = await _reservationService.GetByIdAsync(id);
            if (reservation == null)
                return NotFound(ApiResponse<ReservationResponse>.FailResponse("Reservation not found"));

            var table = await _reservationService.GetTableAsync(id);
            if (table == null)
                return NotFound(ApiResponse<TableResponse>.FailResponse("Table not found"));

            return Ok(ApiResponse<TableResponse>.SuccessResponse(table));
        }

        /// <summary>
        /// Retrieves the reservations handled by a specific customer.
        /// </summary>
        /// <param name="id">Customer ID</param>
        /// <returns>reservations handled by a specific customer.</returns>
        [HttpGet("customer/{id}")]
        public async Task<IActionResult> GetReservationsByCustomerAsync(int id)
        {
            var reservation = await _reservationService.GetByIdAsync(id);
            if (reservation == null)
                return NotFound(ApiResponse<ReservationResponse>.FailResponse("Reservation not found"));

            var reservationsByCustomer = await _reservationService.GetReservationsByCustomerAsync(id);
            if (reservationsByCustomer == null)
                return NotFound(ApiResponse<TableResponse>.FailResponse("Theres no reservations for this Customer"));

            return Ok(ApiResponse<IEnumerable<ReservationResponse>>.SuccessResponse(reservationsByCustomer));
        }

        /// <summary>
        /// Retrieves all Ordered Menu Items handled by the specified reservation.
        /// </summary>
        /// <param name="id">Reservation ID</param>
        /// <returns>List of Ordered Menu Items assigned to the reservation.</returns>
        [HttpGet("{id}/menu-items")]
        public async Task<IActionResult> GetOrderedMenuItemsAsync(int id)
        {
            var menuItems = await _reservationService.GetByIdAsync(id);
            if (menuItems == null)
                return NotFound(ApiResponse<MenuItemResponse>.FailResponse("MenuItem not found"));

            var orderedMenuItems = await _reservationService.GetOrderedMenuItemsAsync(id);
            return Ok(ApiResponse<IEnumerable<MenuItemResponse>>.SuccessResponse(orderedMenuItems));
        }
    }
}