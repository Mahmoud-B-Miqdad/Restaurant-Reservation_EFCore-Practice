using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using RestaurantReservationSystem.API.DTOs.Requests;
using RestaurantReservationSystem.API.DTOs.Responses;
using RestaurantReservationSystem.API.Responses;
using RestaurantReservationSystem.API.Services.Interfaces;

namespace RestaurantReservationSystem.API.Controllers
{
    /// <summary>
    /// Controller for managing orderItem-related operations.
    /// </summary>
    [Route("api/order-items")]
    [ApiController]
    public class OrderItemsController : ControllerBase
    {
        private readonly IOrderItemService _orderItemService;

        public OrderItemsController(IOrderItemService orderItemService)
        {
            _orderItemService = orderItemService;
        }

        /// <summary>
        /// Retrieves all orderItem.
        /// </summary>
        /// <returns>List of all orderItems.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var orderItems = await _orderItemService.GetAllAsync();
            return Ok(ApiResponse<IEnumerable<OrderItemResponse>>.SuccessResponse(orderItems));
        }

        /// <summary>
        /// Retrieves a specific orderItem by ID.
        /// </summary>
        /// <param name="id">OrderItem ID</param>
        /// <returns>The orderItem details if found.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var orderItem = await _orderItemService.GetByIdAsync(id);
            if (orderItem == null)
                return NotFound(ApiResponse<OrderItemResponse>.FailResponse("OrderItem not found"));

            return Ok(ApiResponse<OrderItemResponse>.SuccessResponse(orderItem));
        }

        /// <summary>
        /// Creates a new orderItem.
        /// </summary>
        /// <param name="request">OrderItem creation request payload</param>
        /// <returns>The created orderItem with location header.</returns>
        [HttpPost]
        public async Task<IActionResult> CreateAsync(OrderItemRequest request)
        {
            var createdOrderItem = await _orderItemService.CreateAsync(request);
            CreatedAtAction(
                nameof(GetByIdAsync),
                new { id = createdOrderItem.OrderItemId },
               createdOrderItem);
            return Ok(ApiResponse<OrderItemResponse>.SuccessResponse(createdOrderItem));
        }

        /// <summary>
        /// Updates an existing orderItem completely.
        /// </summary>
        /// <param name="id">OrderItem ID</param>
        /// <param name="request">Updated orderItem data</param>
        /// <returns>The updated orderItem details.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, OrderItemRequest request)
        {
            var updatedOrderItem = await _orderItemService.UpdateAsync(id, request);
            if (updatedOrderItem == null)
                return NotFound(ApiResponse<string>.FailResponse("OrderItem not found"));

            return Ok(ApiResponse<OrderItemResponse>.SuccessResponse(updatedOrderItem));
        }

        /// <summary>
        /// Applies partial update to an orderItem.
        /// </summary>
        /// <param name="id">OrderItem ID</param>
        /// <param name="patchDoc">JSON Patch document</param>
        /// <returns>The updated orderItem details.</returns>
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchAsync(int id, [FromBody] JsonPatchDocument<OrderItemRequest> patchDoc)
        {
            if (patchDoc == null)
                return BadRequest(ApiResponse<string>.FailResponse("Patch document cannot be null"));

            var existingOrderItem = await _orderItemService.GetByIdAsync(id);
            if (existingOrderItem == null)
                return NotFound(ApiResponse<string>.FailResponse("OrderItem not found"));

            var orderItemToPatch = new OrderItemRequest
            {
                OrderId =existingOrderItem.OrderId,
                ItemId =existingOrderItem.ItemId,
                Quantity = existingOrderItem.Quantity
            };

            patchDoc.ApplyTo(orderItemToPatch, ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ApiResponse<string>.FailResponse("Invalid patch document"));

            if (!TryValidateModel(orderItemToPatch))
                return BadRequest(ModelState);

            var updatedOrderItem = await _orderItemService.UpdateAsync(id, orderItemToPatch);
            if (updatedOrderItem == null)
                return NotFound(ApiResponse<string>.FailResponse("Failed to update orderItem"));

            return Ok(ApiResponse<OrderItemResponse>.SuccessResponse(updatedOrderItem));
        }

        /// <summary>
        /// Deletes an orderItem by ID.
        /// </summary>
        /// <param name="id">OrderItem ID</param>
        /// <returns>Success message if deleted.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                var deletedOrderItem = await _orderItemService.DeleteAsync(id);
                if (!deletedOrderItem)
                    return NotFound(ApiResponse<string>.FailResponse("OrderItem not found"));

                return Ok(ApiResponse<string>.SuccessResponse("OrderItem deleted successfully"));

            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ApiResponse<string>.FailResponse(ex.Message));
            }
        }

        /// <summary>
        /// Retrieves the order to which the orderItem belongs.
        /// </summary>
        /// <param name="id">OrderItem ID</param>
        /// <returns>order details of the orderItem.</returns>
        [HttpGet("{id}/order")]
        public async Task<IActionResult> GetOrderAsync(int id)
        {
            var orderItem = await _orderItemService.GetByIdAsync(id);
            if (orderItem == null)
                return NotFound(ApiResponse<OrderItemResponse>.FailResponse("OrderItem not found"));

            var order = await _orderItemService.GetOrderAsync(id);
            if (order == null)
                return NotFound(ApiResponse<OrderResponse>.FailResponse("Order not found"));

            return Ok(ApiResponse<OrderResponse>.SuccessResponse(order));
        }

        /// <summary>
        /// Retrieves the menuItam to which the orderItem belongs.
        /// </summary>
        /// <param name="id">OrderItem ID</param>
        /// <returns>menuItam details of the orderItem.</returns>
        [HttpGet("{id}/menu-item")]
        public async Task<IActionResult> GetMenuItemAsync(int id)
        {
            var orderItem = await _orderItemService.GetByIdAsync(id);
            if (orderItem == null)
                return NotFound(ApiResponse<OrderItemResponse>.FailResponse("OrderItem not found"));

            var menuItem = await _orderItemService.GetMenuItemAsync(id);
            if (menuItem == null)
                return NotFound(ApiResponse<MenuItemResponse>.FailResponse("MenuItem not found"));

            return Ok(ApiResponse<MenuItemResponse>.SuccessResponse(menuItem));
        }
    }
}