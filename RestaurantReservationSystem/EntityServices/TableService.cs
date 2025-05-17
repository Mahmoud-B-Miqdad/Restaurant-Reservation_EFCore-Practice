using Microsoft.EntityFrameworkCore;
using RestaurantReservation.Db.Entities;
using RestaurantReservation.Db.Repositories.Interfaces;
using RestaurantReservationSystem.Constants;
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
            throw new InvalidOperationException(DefaultErrorMessages.AddFailed, ex);
        }
    }
    public async Task<IEnumerable<Table>> GetAllTablesAsync()
    {
        try
        {
            var all = await _tableRepository.GetAllAsync();
            return all;
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException(DefaultErrorMessages.RetrieveFailed, ex);
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
            throw new InvalidOperationException(DefaultErrorMessages.UpdateFailed, ex);
        }
    }

    public async Task DeleteTableAsync(int tableIdToDelete)
    {
        try
        {
            await _tableRepository.DeleteAsync(tableIdToDelete);
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
        await AddTableAsync(
            restaurantId: DefaultTestValues.Id1,
            capacity: DefaultTestValues.DefaultCapacity);

        await UpdateTableAsync(
            tableId: DefaultTestValues.Id1,
            updatedRestaurantId: DefaultTestValues.Id4,
            updatedCapacity: DefaultTestValues.UpdatedCapacity);

        var tables = await GetAllTablesAsync();
        foreach (var table in tables)
        {
            Console.WriteLine($"[Table] {table.TableId} - RestaurantId: {table.RestaurantId}, " +
                $"Capacity: {table.Capacity}");
        }
        await DeleteTableAsync(1);
    }
}
