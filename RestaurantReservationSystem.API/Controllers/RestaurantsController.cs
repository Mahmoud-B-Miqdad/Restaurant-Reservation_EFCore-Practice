using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using RestaurantReservationSystem.API.DTOs.Requests;
using RestaurantReservationSystem.API.DTOs.Responses;
using RestaurantReservationSystem.API.Responses;
using RestaurantReservationSystem.API.Services.Interfaces;

namespace RestaurantReservationSystem.API.Controllers
{
    /// <summary>
    /// API controller for managing restaurants.
    /// Provides endpoints to create, retrieve, update, and delete restaurant data.
    /// </summary>
    [ApiController]
    [Route("api/Restaurants")]
    public class RestaurantsController : ControllerBase
    {
        private readonly IRestaurantService _restaurantService;

        public RestaurantsController(IRestaurantService restaurantService)
        {
            _restaurantService = restaurantService;
        }

        /// <summary>
        /// Retrieves a list of all restaurants.
        /// </summary>
        /// <returns>A list of restaurants wrapped in an API response.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var restaurants = await _restaurantService.GetAllAsync();
            return Ok(ApiResponse<IEnumerable<RestaurantResponse>>.SuccessResponse(restaurants));
        }

        /// <summary>
        /// Retrieves a restaurant by its unique identifier.
        /// </summary>
        /// <param name="id">The ID of the restaurant.</param>
        /// <returns>The restaurant details or a 404 error if not found.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var restaurant = await _restaurantService.GetByIdAsync(id);
            if (restaurant == null)
                return NotFound(ApiResponse<RestaurantResponse>.FailResponse("Restaurant not found"));

            return Ok(ApiResponse<RestaurantResponse>.SuccessResponse(restaurant));
        }

        /// <summary>
        /// Creates a new restaurant.
        /// </summary>
        /// <param name="request">The restaurant details to create.</param>
        /// <returns>The created restaurant with its assigned ID.</returns>
        [HttpPost]
        public async Task<IActionResult> Create(RestaurantRequest request)
        {
            var created = await _restaurantService.CreateAsync(request);
            return CreatedAtAction(
                nameof(GetById),
                new { id = created.RestaurantId },
                ApiResponse<RestaurantResponse>.SuccessResponse(created));
        }

        /// <summary>
        /// Updates an existing restaurant.
        /// </summary>
        /// <param name="id">The ID of the restaurant to update.</param>
        /// <param name="request">The updated restaurant details.</param>
        /// <returns>The updated restaurant or 404 if not found.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, RestaurantRequest request)
        {
            var updated = await _restaurantService.UpdateAsync(id, request);
            if (updated == null)
                return NotFound(ApiResponse<RestaurantResponse>.FailResponse("Restaurant not found"));

            return Ok(ApiResponse<RestaurantResponse>.SuccessResponse(updated));
        }

        /// <summary>
        /// Applies a JSON Patch to a restaurant.
        /// </summary>
        /// <param name="id">The ID of the restaurant to patch.</param>
        /// <param name="patchDoc">The JSON Patch document containing the operations.</param>
        /// <returns>The updated restaurant or appropriate error messages.</returns>
        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, [FromBody] JsonPatchDocument<RestaurantRequest> patchDoc)
        {
            if (patchDoc == null)
                return BadRequest(ApiResponse<string>.FailResponse("Patch document cannot be null"));

            var existingRestaurant = await _restaurantService.GetByIdAsync(id);
            if (existingRestaurant == null)
                return NotFound(ApiResponse<string>.FailResponse("Restaurant not found"));

            var restaurantToPatch = new RestaurantRequest
            {
                Name = existingRestaurant.Name,
                Address = existingRestaurant.Address,
                PhoneNumber = existingRestaurant.PhoneNumber,
                OpeningHours = existingRestaurant.OpeningHours
            };

            patchDoc.ApplyTo(restaurantToPatch, ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ApiResponse<string>.FailResponse("Invalid patch document"));

            if (!TryValidateModel(restaurantToPatch))
                return BadRequest(ModelState);

            var updated = await _restaurantService.UpdateAsync(id, restaurantToPatch);
            if (updated == null)
                return NotFound(ApiResponse<string>.FailResponse("Failed to update restaurant"));

            return Ok(ApiResponse<RestaurantResponse>.SuccessResponse(updated));
        }

        /// <summary>
        /// Deletes a restaurant by ID.
        /// </summary>
        /// <param name="id">The ID of the restaurant to delete.</param>
        /// <returns>Success message or 404 if not found.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _restaurantService.DeleteAsync(id);
            if (!deleted)
                return NotFound(ApiResponse<string>.FailResponse("Restaurant not found"));

            return Ok(ApiResponse<string>.SuccessResponse("Restaurant deleted successfully"));
        }

        /// <summary>
        /// Retrieves all employees working at the specified restaurant.
        /// </summary>
        /// <param name="id">The unique identifier of the restaurant.</param>
        /// <returns>
        /// An <see cref="IActionResult"/> containing a successful <see cref="ApiResponse{T}"/> with a list
        /// of <see cref="EmployeeResponse"/> if the restaurant exists; otherwise, a not found response.
        /// </returns>
        [HttpGet("{id}/employees")]
        public async Task<IActionResult> GetEmployees(int id)
        {
            var employees = await _restaurantService.GetEmployeesAsync(id);
            return Ok(ApiResponse<IEnumerable<EmployeeResponse>>.SuccessResponse(employees));
        }

    }
}