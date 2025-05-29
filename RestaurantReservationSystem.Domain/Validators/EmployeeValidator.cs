using RestaurantReservationSystem.Domain.Exceptions;
using RestaurantReservationSystem.Domain.Interfaces.Repositories;
using RestaurantReservationSystem.Domain.Models;



namespace RestaurantReservationSystem.Domain.Validators
{
    public class EmployeeValidator
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeValidator(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        public async Task<EmployeeModel> EnsureEmployeeExistsAsync(int employeeId)
        {
            var employee = await _employeeRepository.GetByIdAsync(employeeId);
            if (employee == null)
                throw new NotFoundException($"Employee with ID {employeeId} not found");

            return employee;
        }
    }
}
