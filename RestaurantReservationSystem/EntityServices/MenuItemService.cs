using RestaurantReservation.Db.Models;
using RestaurantReservation.Db.Repositories;

public class MenuItemService
{
    private readonly MenuItemRepository _menuItemRepository;

    public MenuItemService(MenuItemRepository menuItemRepository)
    {
        _menuItemRepository = menuItemRepository;
    }

    public async Task AddMenuItemAsync()
    {
        var menuItem = new MenuItem
        {
            RestaurantId = 1,
            Name = "Grilled Chicken",
            Description = "Delicious grilled chicken served with vegetables.",
            Price = 12.99m
        };

        try
        {
            await _menuItemRepository.AddAsync(menuItem);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Failed to add the menu item.", ex);
        }
    }

    public async Task GetAllMenuItemsAsync()
    {
        try
        {
            var allItems = await _menuItemRepository.GetAllAsync();
            foreach (var item in allItems)
            {
                Console.WriteLine($"[MenuItem] {item.ItemId} - Name: {item.Name}, Price: {item.Price}, RestaurantId: {item.RestaurantId}");
            }
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Failed to retrieve menu items from the database.", ex);
        }
    }

    public async Task UpdateMenuItemAsync()
    {
        var updatedItem = new MenuItem
        {
            ItemId = 1, 
            RestaurantId = 1,
            Name = "Grilled Chicken (Updated)",
            Description = "Updated description for grilled chicken.",
            Price = 14.99m
        };

        try
        {
            await _menuItemRepository.UpdateAsync(updatedItem);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Failed to update the menu item.", ex);
        }
    }

    public async Task DeleteMenuItemAsync()
    {
        int itemIdToDelete = 1; 

        try
        {
            await _menuItemRepository.DeleteAsync(itemIdToDelete);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Failed to delete the menu item.", ex);
        }
    }

    public async Task ExecuteExamplesAsync()
    {
        await AddMenuItemAsync();
        await UpdateMenuItemAsync();
        await GetAllMenuItemsAsync();
        await DeleteMenuItemAsync();
    }
}
