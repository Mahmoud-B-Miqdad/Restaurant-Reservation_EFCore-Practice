using RestaurantReservation.Db.Entities;

namespace RestaurantReservation.Db.Services.Interfaces
{
    public interface ICustomerService
    {
        Task AddCustomerAsync(string firstName, string lastName, string email, string phoneNumber);
        Task UpdateCustomerAsync(int id, string UpdatedfirstName, string UpdatedlastName,
        string Updatedemail, string UpdatedphoneNumber);
        Task<IEnumerable<Customer>> GetAllCustomersAsync();
        Task DeleteCustomerAsync(int customerIdToDelete);
        Task ExecuteExamplesAsync();
    }
}