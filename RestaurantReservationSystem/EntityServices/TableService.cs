using RestaurantReservation.Db.Models;
using RestaurantReservation.Db.Repositories.Interfaces;
using RestaurantReservationSystem.EntityServices.Services.Interfaces;

public class TableService : ITableService
{
    private readonly ITableRepository _tableRepository;

    public TableService(ITableRepository tableRepository)
    {
        _tableRepository = tableRepository;
    }

    public async Task AddTableAsync(int restaurantId, int capacity)
    {
        var table = new Table
        {
            RestaurantId = restaurantId,
            Capacity = capacity
        };

        try
        {
            await _tableRepository.AddAsync(table);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Failed to add the table.", ex);
        }
    }
    public async Task GetAllTablesAsync()
    {
        try
        {
            var all = await _tableRepository.GetAllAsync();
            foreach (var table in all)
            {
                Console.WriteLine($"[Table] {table.TableId} - RestaurantId: {table.RestaurantId}, " +
                    $"Capacity: {table.Capacity}");
            }
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Failed to retrieve tables from the database.", ex);
        }
    }

    public async Task UpdateTableAsync(int tableId, int updatedRestaurantId, int updatedCapacity)
    {
        var table = new Table
        {
            TableId = tableId,
            RestaurantId = updatedRestaurantId,
            Capacity = updatedCapacity
        };

        try
        {
            await _tableRepository.UpdateAsync(table);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Failed to update the table.", ex);
        }
    }

    public async Task DeleteTableAsync()
    {
        int tableIdToDelete = 1; 

        try
        {
            await _tableRepository.DeleteAsync(tableIdToDelete);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Failed to delete the table.", ex);
        }
    }

    public async Task ExecuteExamplesAsync()
    {
        await AddTableAsync(
            restaurantId: 1,
            capacity: 4);

        await UpdateTableAsync(
            tableId: 1,
            updatedRestaurantId: 4,
            updatedCapacity: 6);

        await GetAllTablesAsync();
        await DeleteTableAsync();
    }
}
