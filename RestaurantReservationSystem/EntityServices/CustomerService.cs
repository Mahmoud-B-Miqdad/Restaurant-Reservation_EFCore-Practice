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

    public async Task AddCustomerAsync()
    {
        var customer = new Customer
        {
            FirstName = "Test",
            LastName = "Test",
            Email = "Test@gmail.com",
            PhoneNumber = "1234567890"
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

    public async Task UpdateCustomerAsync()
    {
        var customer = new Customer
        {
            CustomerId = 1,
            FirstName = "Updated FirstName",
            LastName = "Updated LastName",
            PhoneNumber = "0599999999",
            Email = "Updated Test@gmail.com"
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
        await AddCustomerAsync();
        await UpdateCustomerAsync();
        await GetAllCustomersAsync();
        await DeleteCustomerAsync();
    }
}