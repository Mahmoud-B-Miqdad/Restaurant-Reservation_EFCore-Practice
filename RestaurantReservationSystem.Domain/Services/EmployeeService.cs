using AutoMapper;
using RestaurantReservationSystem.Domain.DTOs.Requests;
using RestaurantReservationSystem.Domain.DTOs.Responses;
using RestaurantReservationSystem.Domain.Interfaces.Repositories;
using RestaurantReservationSystem.Domain.Interfaces.Services;
using RestaurantReservationSystem.Domain.Models;

namespace RestaurantReservationSystem.Domain.Services
{
    /// <summary>
    /// Provides business logic and service methods for managing employee operations.
    /// </summary>
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="EmployeeService"/> class.
        /// </summary>
        /// <param name="repository">The repository responsible for employee data access.</param>
        /// <param name="mapper">The mapper used to convert between entities and DTOs.</param>
        public EmployeeService(IEmployeeRepository repository, IMapper mapper)
        {
            _employeeRepository = repository;
            _mapper = mapper;
        }

        /// <inheritdoc />
        public async Task<List<EmployeeResponse>> ListManagersAsync()
        {
            var managers = await _employeeRepository.ListManagersAsync();
            return _mapper.Map<List<EmployeeResponse>>(managers);
        }

        /// <inheritdoc />
        public async Task<List<EmployeeResponse>> GetAllAsync()
        {
            var employees = await _employeeRepository.GetAllAsync();
            return _mapper.Map<List<EmployeeResponse>>(employees);
        }

        /// <inheritdoc />
        public async Task<EmployeeResponse?> GetByIdAsync(int id)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);
            return employee == null ? null : _mapper.Map<EmployeeResponse>(employee);
        }

        /// <inheritdoc />
        public async Task<EmployeeResponse> CreateAsync(EmployeeRequest request)
        {
            var employee = _mapper.Map<EmployeeModel>(request);
            await _employeeRepository.AddAsync(employee);
            return _mapper.Map<EmployeeResponse>(employee);
        }

        /// <inheritdoc />
        public async Task<EmployeeResponse?> UpdateAsync(int id, EmployeeRequest request)
        {
            var updatedEmployee = await _employeeRepository.GetByIdAsync(id);
            if (updatedEmployee == null) return null;

            _mapper.Map(request, updatedEmployee);
            await _employeeRepository.UpdateAsync(updatedEmployee);
            return _mapper.Map<EmployeeResponse>(updatedEmployee);
        }

        /// <inheritdoc />
        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _employeeRepository.GetByIdAsync(id);
            if (existing == null) return false;

                await _employeeRepository.DeleteAsync(id);
                return true;
        }

        /// <inheritdoc />
        public async Task<List<EmployeeResponse>> GetEmployeesByRestaurantIdAsync(int restaurantId)
        {
            var employees = await _employeeRepository.GetByRestaurantIdAsync(restaurantId);
            return _mapper.Map<List<EmployeeResponse>>(employees);
        }

        /// <inheritdoc />
        public async Task<EmployeeResponse?> GetEmployeeByOrderIdAsync(int orderId)
        {
            var employee = await _employeeRepository.GetEmployeeByOrderIdAsync(orderId);
            return employee == null ? null : _mapper.Map<EmployeeResponse>(employee);
        }

    }
}