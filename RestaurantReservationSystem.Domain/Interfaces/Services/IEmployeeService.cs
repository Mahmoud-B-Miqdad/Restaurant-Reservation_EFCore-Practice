using RestaurantReservationSystem.API.DTOs.Requests;
using RestaurantReservationSystem.API.DTOs.Responses;

namespace RestaurantReservationSystem.Domain.Interfaces.Services
{
    /// <summary>
    /// Defines the contract for managing employee-related operations.
    /// </summary>
    public interface IEmployeeService
    {
        /// <summary>
        /// Retrieves a list of all Managers.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation.
        /// The task result contains a list of managers employee response DTOs.
        /// </returns>

        Task<List<EmployeeResponse>> ListManagersAsync();

        /// <summary>
        /// Retrieves a list of all employees.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains a list of employee response DTOs.</returns>
        Task<List<EmployeeResponse>> GetAllAsync();

        /// <summary>
        /// Retrieves a specific employee by their unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the employee.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the employee response DTO if found; otherwise, null.</returns>
        Task<EmployeeResponse?> GetByIdAsync(int id);

        /// <summary>
        /// Adds a new employee to the system.
        /// </summary>
        /// <param name="request">The request DTO containing employee details.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task<EmployeeResponse> CreateAsync(EmployeeRequest request);

        /// <summary>
        /// Updates the details of an existing employee.
        /// </summary>
        /// <param name="id">The unique identifier of the employee to update.</param>
        /// <param name="request">The request DTO containing updated employee information.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task<EmployeeResponse?> UpdateAsync(int id, EmployeeRequest request);

        /// <summary>
        /// Deletes an employee from the system.
        /// </summary>
        /// <param name="id">The unique identifier of the employee to delete.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public Task<bool> DeleteAsync(int id);

        /// <summary>
        /// Retrieves all orders handled by a specific employee.
        /// </summary>
        /// <param name="employeeId">The unique identifier of the employee.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a list of order response DTOs.</returns>
        Task<List<OrderResponse>> GetOrdersAsync(int employeeId);

        /// <summary>
        /// Retrieves the restaurant associated with a specific employee.
        /// </summary>
        /// <param name="employeeId">The unique identifier of the employee.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the restaurant response DTO if found; otherwise, null.</returns>
        Task<RestaurantResponse?> GetRestaurantAsync(int employeeId);
    }
}