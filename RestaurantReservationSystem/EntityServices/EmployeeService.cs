using RestaurantReservation.Db.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class EmployeeService
{
    private readonly EmployeeOperations _employeeOperations;

    public EmployeeService(EmployeeOperations employeeOperations)
    {
        _employeeOperations = employeeOperations;
    }

    public async Task AddEmployeeAsync()
    {
        var employee = new Employee
        {
            FirstName = "Test",
            LastName = "Test",
            Position = "Test",
            RestaurantId = 1  
        };

        try
        {
            await _employeeOperations.AddAsync(employee);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Failed to add the employee. Ensure all required fields are valid.", ex);
        }
    }

    public async Task GetAllEmployeesAsync()
    {
        try
        {
            var all = await _employeeOperations.GetAllAsync();
            foreach (var employee in all)
            {
                Console.WriteLine($"[Employee] {employee.FirstName} {employee.LastName}, {employee.Position}");
            }
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Failed to retrieve employees from the database.", ex);
        }
    }

    public async Task UpdateEmployeeAsync()
    {
        var employee = new Employee
        {
            EmployeeId = 1,  
            FirstName = "Updated Test",
            LastName = "Updated Test",
            Position = "Updated Test",
            RestaurantId = 1
        };

        try
        {
            await _employeeOperations.UpdateAsync(employee);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Failed to update the employee. It may have been modified or deleted by another process.", ex);
        }
    }

    public async Task DeleteEmployeeAsync()
    {
        int employeeIdToDelete = 2; 

        try
        {
            await _employeeOperations.DeleteAsync(employeeIdToDelete);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Unexpected error occurred while deleting the employee.", ex);
        }
    }

    public async Task ExecuteExamplesAsync()
    {
        await AddEmployeeAsync();
        await UpdateEmployeeAsync();
        await GetAllEmployeesAsync();
        await DeleteEmployeeAsync();
    }
}
