using RestaurantReservation.Db.Models;
using Microsoft.EntityFrameworkCore;
using RestaurantReservation.Db.Services.Interfaces;
using RestaurantReservation.Db.Repositories.Interfaces;

public class CustomerService : ICustomerService 
{
    private readonly ICustomerRepository _customerRepository;

    public CustomerService(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task AddCustomerAsync(string firstName, string lastName, string email, string phoneNumber)
    {
        var customer = new Customer
        {
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            PhoneNumber = phoneNumber
        };

        try
        {
            await _customerRepository.AddAsync(customer);
        }
        catch (DbUpdateException ex)
        {
            throw new InvalidOperationException("Failed to add the customer. Ensure all required fields are valid.", ex);
        }
    }

    public async Task GetAllCustomersAsync()
    {
        try
        {
            var all = await _customerRepository.GetAllAsync();
            foreach (var customer in all)
            {
                Console.WriteLine($"[Customer] {customer.FirstName} - {customer.LastName}," +
                    $" {customer.Email}, {customer.PhoneNumber}");
            }
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Failed to retrieve customer from the database.", ex);
        }
    }

    public async Task UpdateCustomerAsync(int id, string UpdatedfirstName, string UpdatedlastName, 
        string Updatedemail, string UpdatedphoneNumber)
    {
        var customer = new Customer
        {
            CustomerId = id,
            FirstName = UpdatedfirstName,
            LastName = UpdatedlastName,
            PhoneNumber = Updatedemail,
            Email = UpdatedphoneNumber
        };

        try
        {
            await _customerRepository.UpdateAsync(customer);
        }
        catch (DbUpdateConcurrencyException ex)
        {
            throw new InvalidOperationException("Failed to update the customer. It may have been modified or deleted by another process.", ex);
        }
    }

    public async Task DeleteCustomerAsync()
    {
        int customerIdToDelete = 2;

        try
        {
            await _customerRepository.DeleteAsync(customerIdToDelete);
        }
        catch (DbUpdateException ex)
        {
            throw new InvalidOperationException("Cannot delete the customer because it has related data (e.g., employees or reservations).", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Unexpected error occurred while deleting the customer.", ex);
        }
    }

    public async Task ExecuteExamplesAsync()
    {
        await AddCustomerAsync(
            firstName: "Test",
            lastName: "Test", 
            email: "Test@gmail.com",
            phoneNumber: "0590000000");

        await UpdateCustomerAsync(
            id: 1, 
            UpdatedfirstName: "Updated Test",
            UpdatedlastName: "Updated Test",
            Updatedemail: "UpdatedTest@gmail.com",
            UpdatedphoneNumber: "0599999999");

        await GetAllCustomersAsync();
        await DeleteCustomerAsync();
    }
}