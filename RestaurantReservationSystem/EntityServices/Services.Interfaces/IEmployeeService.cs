namespace RestaurantReservation.Db.Services.Interfaces
{
    public interface IEmployeeService
    {
        Task AddEmployeeAsync();
        Task UpdateEmployeeAsync();
        Task GetAllEmployeesAsync();
        Task DeleteEmployeeAsync();
        Task ExecuteExamplesAsync();
    }
}