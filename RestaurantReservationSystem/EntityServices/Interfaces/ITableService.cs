namespace RestaurantReservationSystem.EntityServices.Services.Interfaces
{
    public interface ITableService
    {
        Task AddTableAsync(int restaurantId, int capacity);
        Task UpdateTableAsync(int tableId, int updatedRestaurantId, int updatedCapacity);
        Task GetAllTablesAsync();
        Task DeleteTableAsync();
        Task ExecuteExamplesAsync();
    }
}