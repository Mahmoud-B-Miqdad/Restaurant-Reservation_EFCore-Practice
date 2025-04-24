using Microsoft.EntityFrameworkCore;
using RestaurantReservation.Db.Models;
using RestaurantReservation.Db.Repositories.Interfaces;
using RestaurantReservation.Db.Services.Interfaces;
using RestaurantReservationSystem.Constants;

public class EmployeeService : IEmployeeService
{
    private readonly IEmployeeRepository _employeeOperations;

    public EmployeeService(IEmployeeRepository employeeOperations)
    {
        _employeeOperations = employeeOperations;
    }

    public async Task AddEmployeeAsync(string firstName, string lastName, string position, int restaurantId)
{
    var employee = new Employee
    {
        FirstName = firstName,
        LastName = lastName,
        Position = position,
        RestaurantId = restaurantId
    };

        try
        {
            await _employeeOperations.AddAsync(employee);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException(DefaultErrorMessages.AddFailed, ex);
        }
    }

    public async Task<IEnumerable<Employee>> GetAllEmployeesAsync()
    {
        try
        {
            var all = await _employeeOperations.GetAllAsync();
            return all;
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException(DefaultErrorMessages.RetrieveFailed, ex);
        }
    }

    public async Task UpdateEmployeeAsync(int employeeId, string UpdatedfirstName, 
        string UpdatedlastName, string Updatedposition, int UpdatedrestaurantId)
    {

        var employee = new Employee
        {
            EmployeeId = employeeId,
            FirstName = UpdatedfirstName,
            LastName = UpdatedlastName,
            Position = Updatedposition,
            RestaurantId = UpdatedrestaurantId
        };


        try
        {
            await _employeeOperations.UpdateAsync(employee);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException(DefaultErrorMessages.UpdateFailed, ex);
        }
    }

    public async Task DeleteEmployeeAsync()
    {
        int employeeIdToDelete = 2; 

        try
        {
            await _employeeOperations.DeleteAsync(employeeIdToDelete);
        }
        catch (DbUpdateException ex)
        {
            throw new InvalidOperationException(DefaultErrorMessages.DeleteWithRelations, ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException(DefaultErrorMessages.DeleteUnexpected, ex);
        }
    }

    public async Task ExecuteExamplesAsync()
    {
        await AddEmployeeAsync(
            firstName: DefaultTestValues.DefaultName,
            lastName: DefaultTestValues.DefaultName,
            position: DefaultTestValues.DefaultPosition,
            restaurantId: DefaultTestValues.Id2);

        await UpdateEmployeeAsync(
            employeeId: DefaultTestValues.Id1,
            UpdatedfirstName: DefaultTestValues.UpdatedName,
            UpdatedlastName: DefaultTestValues.UpdatedName,
            Updatedposition: DefaultTestValues.UpdatedPosition,
            UpdatedrestaurantId: DefaultTestValues.Id4);

        var employees = await GetAllEmployeesAsync();
        foreach (var employee in employees)
        {
            Console.WriteLine($"[Employee] {employee.FirstName} {employee.LastName}, {employee.Position}");
        }
        await DeleteEmployeeAsync();
    }
}
