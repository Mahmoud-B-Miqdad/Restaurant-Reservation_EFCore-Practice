namespace RestaurantReservationSystem.EntityServices.Services.Interfaces
{
    public interface ITableService
    {
        Task AddTableAsync();
        Task UpdateTableAsync();
        Task GetAllTablesAsync();
        Task DeleteTableAsync();
        Task ExecuteExamplesAsync();
    }
}