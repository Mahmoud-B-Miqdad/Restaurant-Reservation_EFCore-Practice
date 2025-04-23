namespace RestaurantReservation.Db.Services.Interfaces
{
    public interface ICustomerService
    {
        Task AddCustomerAsync();
        Task UpdateCustomerAsync();
        Task GetAllCustomersAsync();
        Task DeleteCustomerAsync();
        Task ExecuteExamplesAsync();
    }
}