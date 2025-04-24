namespace RestaurantReservation.Db.Services.Interfaces
{
    public interface IEmployeeService
    {
        Task AddEmployeeAsync(string firstName, string lastName, string position, int restaurantId);
        Task UpdateEmployeeAsync(int employeeId, string UpdatedfirstName,
        string UpdatedlastName, string Updatedposition, int UpdatedrestaurantId);
        Task GetAllEmployeesAsync();
        Task DeleteEmployeeAsync();
        Task ExecuteExamplesAsync();
    }
}