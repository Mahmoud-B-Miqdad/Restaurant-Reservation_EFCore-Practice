using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using RestaurantReservationSystem.API.DTOs;
using RestaurantReservationSystem.API.Responses;
using RestaurantReservationSystem.API.Services.Interfaces;

namespace RestaurantReservationSystem.API.Controllers
{
    [ApiController]
    [Route("api/Restaurants")]
    public class RestaurantsController : ControllerBase
    {
        private readonly IRestaurantService _restaurantService;

        public RestaurantsController(IRestaurantService restaurantService)
        {
            _restaurantService = restaurantService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var restaurants = await _restaurantService.GetAllAsync();
            return Ok(ApiResponse<IEnumerable<RestaurantResponse>>.SuccessResponse(restaurants));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var restaurant = await _restaurantService.GetByIdAsync(id);
            if (restaurant == null)
                return NotFound(ApiResponse<RestaurantResponse>.FailResponse("Restaurant not found"));

            return Ok(ApiResponse<RestaurantResponse>.SuccessResponse(restaurant));
        }

        [HttpPost]
        public async Task<IActionResult> Create(RestaurantRequest request)
        {
            var created = await _restaurantService.CreateAsync(request);
            return CreatedAtAction(
                nameof(GetById), 
                new { id = created.RestaurantId }, 
                ApiResponse<RestaurantResponse>.SuccessResponse(created));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, RestaurantRequest request)
        {
            var updated = await _restaurantService.UpdateAsync(id, request);
            if (updated == null)
                return NotFound(ApiResponse<RestaurantResponse>.FailResponse("Restaurant not found"));

            return Ok(ApiResponse<RestaurantResponse>.SuccessResponse(updated));
        }

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

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _restaurantService.DeleteAsync(id);
            if (!deleted)
                return NotFound(ApiResponse<string>.FailResponse("Restaurant not found"));

            return Ok(ApiResponse<string>.SuccessResponse("Restaurant deleted successfully"));
        }
    }
}
