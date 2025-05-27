using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using RestaurantReservationSystem.Domain.DTOs.Requests;
using RestaurantReservationSystem.Domain.DTOs.Responses;
using RestaurantReservationSystem.Domain.Interfaces.Services;
using RestaurantReservationSystem.Domain.Responses;

namespace RestaurantReservationSystem.API.Controllers
{
    /// <summary>
    /// Controller for managing employee-related operations.
    /// </summary>
    [ApiController]
    [Route("api/employees")]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly IRestaurantService _restaurantService;

        public EmployeesController(IEmployeeService employeSservice, IRestaurantService restaurantService)
        {
            _employeeService = employeSservice;
            _restaurantService = restaurantService;
        }

        /// <summary>
        /// Retrieves all employees or only managers if requested.
        /// </summary>
        /// <param name="managersOnly">If true, returns only managers.</param>
        /// <returns>List of employees.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] bool managersOnly = false)
        {
            IEnumerable<EmployeeResponse> employees;

            if (managersOnly)
            {
                employees = await _employeeService.ListManagersAsync();
            }
            else
            {
                employees = await _employeeService.GetAllAsync();
            }

            return Ok(ApiResponse<IEnumerable<EmployeeResponse>>.SuccessResponse(employees));
        }


        /// <summary>
        /// Retrieves a specific employee by ID.
        /// </summary>
        /// <param name="id">Employee ID</param>
        /// <returns>The employee details if found.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var employee = await _employeeService.GetByIdAsync(id);

            return Ok(ApiResponse<EmployeeResponse>.SuccessResponse(employee));
        }

        /// <summary>
        /// Creates a new employee.
        /// </summary>
        /// <param name="request">Employee creation request payload</param>
        /// <returns>The created employee with location header.</returns>
        [HttpPost]
        public async Task<IActionResult> CreateAsync(EmployeeRequest request)
        {
            var createdEmployee = await _employeeService.CreateAsync(request);
            return CreatedAtAction(
                nameof(GetByIdAsync),
                new { id = createdEmployee.EmployeeId },
                ApiResponse<EmployeeResponse>.SuccessResponse(createdEmployee));
        }

        /// <summary>
        /// Updates an existing employee completely.
        /// </summary>
        /// <param name="id">Employee ID</param>
        /// <param name="request">Updated employee data</param>
        /// <returns>The updated employee details.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, EmployeeRequest request)
        {
            var updatedEmployee = await _employeeService.UpdateAsync(id, request);

            return Ok(ApiResponse<EmployeeResponse>.SuccessResponse(updatedEmployee));
        }

        /// <summary>
        /// Applies partial update to an employee.
        /// </summary>
        /// <param name="id">Employee ID</param>
        /// <param name="patchDoc">JSON Patch document</param>
        /// <returns>The updated employee details.</returns>
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchAsync(int id, [FromBody] JsonPatchDocument<EmployeeRequest> patchDoc)
        {
            if (patchDoc == null)
                return BadRequest(ApiResponse<string>.FailResponse("Patch document cannot be null"));

            var existingEmployee = await _employeeService.GetByIdAsync(id);

            var employeeToPatch = new EmployeeRequest
            {
                RestaurantId = existingEmployee.RestaurantId,
                FirstName = existingEmployee.FirstName,
                LastName = existingEmployee.LastName,
                Position = existingEmployee.Position
            };

            patchDoc.ApplyTo(employeeToPatch, ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ApiResponse<string>.FailResponse("Invalid patch document"));

            if (!TryValidateModel(employeeToPatch))
                return BadRequest(ModelState);

            var updatedEmployee = await _employeeService.UpdateAsync(id, employeeToPatch);
            if (updatedEmployee == null)
                return NotFound(ApiResponse<string>.FailResponse("Failed to update employee"));

            return Ok(ApiResponse<EmployeeResponse>.SuccessResponse(updatedEmployee));
        }

        /// <summary>
        /// Deletes an employee by ID.
        /// </summary>
        /// <param name="id">Employee ID</param>
        /// <returns>Success message if deleted.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var deletedEmployee = await _employeeService.DeleteAsync(id);

            return Ok(ApiResponse<string>.SuccessResponse("Employee deleted successfully"));
        }

        /// <summary>
        /// Retrieves all orders handled by the specified employee.
        /// </summary>
        /// <param name="id">Employee ID</param>
        /// <returns>List of orders assigned to the employee.</returns>
        [HttpGet("{id}/orders")]
        public async Task<IActionResult> GetOrdersAsync(int id)
        {
            var employee = await _employeeService.GetByIdAsync(id);

            var orders = await _employeeService.GetOrdersByEmployeeIdAsync(id);
            return Ok(ApiResponse<List<OrderResponse>>.SuccessResponse(orders));
        }

        /// <summary>
        /// Retrieves the restaurant to which the employee belongs.
        /// </summary>
        /// <param name="id">Employee ID</param>
        /// <returns>Restaurant details of the employee.</returns>
        [HttpGet("{id}/restaurant")]
        public async Task<IActionResult> GetRestaurantAsync(int id)
        {
            var employee = await _employeeService.GetByIdAsync(id);

            var restaurant = await _restaurantService.GetRestaurantByEmployeeIdAsync(id);

            return Ok(ApiResponse<RestaurantResponse>.SuccessResponse(restaurant));
        }
    }
}