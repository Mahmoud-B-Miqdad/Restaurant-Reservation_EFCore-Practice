using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using RestaurantReservationSystem.API.DTOs.Requests;
using RestaurantReservationSystem.API.DTOs.Responses;
using RestaurantReservationSystem.API.Responses;
using RestaurantReservationSystem.API.Services.Interfaces;

namespace RestaurantReservationSystem.API.Controllers
{
    /// <summary>
    /// Controller for managing menuItem-related operations.
    /// </summary>
    [Route("api/menu-item")]
    [ApiController]
    public class MenuItemController : ControllerBase
    {
        private readonly IMenuItemService _menuItemService;

        public MenuItemController(IMenuItemService menuItemService)
        {
            _menuItemService = menuItemService;
        }

        /// <summary>
        /// Retrieves all menuItem.
        /// </summary>
        /// <returns>List of all menuItem.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var menuItem = await _menuItemService.GetAllAsync();
            return Ok(ApiResponse<IEnumerable<MenuItemResponse>>.SuccessResponse(menuItem));
        }

        /// <summary>
        /// Retrieves a specific menuItem by ID.
        /// </summary>
        /// <param name="id">menuItem ID</param>
        /// <returns>The menuItem details if found.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var menuItem = await _menuItemService.GetByIdAsync(id);
            if (menuItem == null)
                return NotFound(ApiResponse<MenuItemResponse>.FailResponse("MenuItem not found"));

            return Ok(ApiResponse<MenuItemResponse>.SuccessResponse(menuItem));
        }

        /// <summary>
        /// Creates a new menuItem.
        /// </summary>
        /// <param name="request">MenuItem creation request payload</param>
        /// <returns>The created menuItem with location header.</returns>
        [HttpPost]
        public async Task<IActionResult> CreateAsync(MenuItemRequest request)
        {
            var createdMenuItem = await _menuItemService.CreateAsync(request);
            CreatedAtAction(
                nameof(GetByIdAsync),
                new { id = createdMenuItem},
               createdMenuItem);
            return Ok(ApiResponse<MenuItemResponse>.SuccessResponse(createdMenuItem));
        }

        /// <summary>
        /// Updates an existing menuItem completely.
        /// </summary>
        /// <param name="id">MenuItem ID</param>
        /// <param name="request">Updated menuItem data</param>
        /// <returns>The updated menuItem details.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, MenuItemRequest request)
        {
            var updatedMenuItem = await _menuItemService.UpdateAsync(id, request);
            if (updatedMenuItem == null)
                return NotFound(ApiResponse<string>.FailResponse("MenuItem not found"));

            return Ok(ApiResponse<MenuItemResponse>.SuccessResponse(updatedMenuItem));
        }

        /// <summary>
        /// Applies partial update to an menuItem.
        /// </summary>
        /// <param name="id">MenuItem ID</param>
        /// <param name="patchDoc">JSON Patch document</param>
        /// <returns>The updated menuItem details.</returns>
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchAsync(int id, [FromBody] JsonPatchDocument<MenuItemRequest> patchDoc)
        {
            if (patchDoc == null)
                return BadRequest(ApiResponse<string>.FailResponse("Patch document cannot be null"));

            var existingMenuItem = await _menuItemService.GetByIdAsync(id);
            if (existingMenuItem == null)
                return NotFound(ApiResponse<string>.FailResponse("MenuItem not found"));

            var menuItemToPatch = new MenuItemRequest
            {
                Name= existingMenuItem.Name,
                Description = existingMenuItem.Description,
                Price = existingMenuItem.Price,
            };

            patchDoc.ApplyTo(menuItemToPatch, ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ApiResponse<string>.FailResponse("Invalid patch document"));

            if (!TryValidateModel(menuItemToPatch))
                return BadRequest(ModelState);

            var updatedMenuItem = await _menuItemService.UpdateAsync(id, menuItemToPatch);
            if (updatedMenuItem == null)
                return NotFound(ApiResponse<string>.FailResponse("Failed to update menuItem"));

            return Ok(ApiResponse<MenuItemResponse>.SuccessResponse(updatedMenuItem));
        }

        /// <summary>
        /// Deletes an menuItem by ID.
        /// </summary>
        /// <param name="id">MenuItem ID</param>
        /// <returns>Success message if deleted.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var deletedEmployee = await _menuItemService.DeleteAsync(id);
            if (!deletedEmployee)
                return NotFound(ApiResponse<string>.FailResponse("MenuItem not found"));

            return Ok(ApiResponse<string>.SuccessResponse("menuItem deleted successfully"));
        }

        /// <summary>
        /// Retrieves all order items handled by the specified menuItem.
        /// </summary>
        /// <param name="id">MenuItem ID</param>
        /// <returns>List of orders assigned to the menuItem.</returns>
        [HttpGet("{id}/order-items")]
        public async Task<IActionResult> GetOrderItemsAsync(int id)
        {
            var menuItem = await _menuItemService.GetByIdAsync(id);
            if (menuItem == null)
                return NotFound(ApiResponse<MenuItemResponse>.FailResponse("MenuItem not found"));

            var orders = await _menuItemService.GetOrderItemsAsync(id);
            return Ok(ApiResponse<IEnumerable<OrderItemResponse>>.SuccessResponse(orders));
        }

        /// <summary>
        /// Retrieves the restaurant to which the menuItem belongs.
        /// </summary>
        /// <param name="id">MenuItem ID</param>
        /// <returns>Restaurant details of the menuItem.</returns>
        [HttpGet("{id}/restaurant")]
        public async Task<IActionResult> GetRestaurantAsync(int id)
        {
            var menuItem = await _menuItemService.GetByIdAsync(id);
            if (menuItem == null)
                return NotFound(ApiResponse<MenuItemResponse>.FailResponse("MenuItem not found"));

            var restaurant = await _menuItemService.GetRestaurantAsync(id);
            if (restaurant == null)
                return NotFound(ApiResponse<RestaurantResponse>.FailResponse("Restaurant not found"));

            return Ok(ApiResponse<RestaurantResponse>.SuccessResponse(restaurant));
        }
    }
}