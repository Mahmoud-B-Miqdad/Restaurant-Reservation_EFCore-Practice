using RestaurantReservation.Db.Models;

public class TableService
{
    private readonly TableRepository _tableRepository;

    public TableService(TableRepository tableRepository)
    {
        _tableRepository = tableRepository;
    }

    public async Task AddTableAsync()
    {
        var table = new Table
        {
            RestaurantId = 1, 
            Capacity = 4
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

    public async Task UpdateTableAsync()
    {
        var table = new Table
        {
            TableId = 1, 
            RestaurantId = 4, 
            Capacity = 6
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
        //await AddTableAsync();
        await UpdateTableAsync();
        //await GetAllTablesAsync();
        //await DeleteTableAsync();
    }
}
