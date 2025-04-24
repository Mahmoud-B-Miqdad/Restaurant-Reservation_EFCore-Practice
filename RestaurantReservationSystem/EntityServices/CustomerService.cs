using RestaurantReservation.Db.Models;
using Microsoft.EntityFrameworkCore;
using RestaurantReservation.Db.Services.Interfaces;
using RestaurantReservation.Db.Repositories.Interfaces;
using RestaurantReservationSystem.Constants;

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
            throw new InvalidOperationException(DefaultErrorMessages.AddFailed, ex);
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
            throw new InvalidOperationException(DefaultErrorMessages.RetrieveFailed, ex);
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
            throw new InvalidOperationException(DefaultErrorMessages.UpdateFailed, ex);
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
            throw new InvalidOperationException(DefaultErrorMessages.DeleteWithRelations, ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException(DefaultErrorMessages.DeleteUnexpected, ex);
        }
    }

    public async Task ExecuteExamplesAsync()
    {
        await AddCustomerAsync(
            firstName: DefaultTestValues.DefaultName,
            lastName: DefaultTestValues.DefaultName,
            email: DefaultTestValues.DefaultEmail,
            phoneNumber: DefaultTestValues.DefaultPhoneNumber);

        await UpdateCustomerAsync(
            id: DefaultTestValues.Id1,
            UpdatedfirstName: DefaultTestValues.UpdatedName,
            UpdatedlastName: DefaultTestValues.UpdatedName,
            Updatedemail: DefaultTestValues.UpdatedEmail,
            UpdatedphoneNumber: DefaultTestValues.UpdatedPhoneNumber);

        await GetAllCustomersAsync();
        await DeleteCustomerAsync();
    }
}