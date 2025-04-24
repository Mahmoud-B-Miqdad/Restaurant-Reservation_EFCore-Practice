using RestaurantReservation.Db.Models;
using RestaurantReservation.Db.Repositories.Interfaces;
using RestaurantReservation.Db.Services.Interfaces;

public class MenuItemService : IMenuItemService
{
    private readonly IMenuItemRepository _menuItemRepository;

    public MenuItemService(IMenuItemRepository menuItemRepository)
    {
        _menuItemRepository = menuItemRepository;
    }

    public async Task AddMenuItemAsync(int restaurantId, string name, string description, decimal price)
    {
        var menuItem = new MenuItem
        {
            RestaurantId = restaurantId,
            Name = name,
            Description = description,
            Price = price
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

    public async Task UpdateMenuItemAsync(int itemId, int UpdatedrestaurantId, string Updatedname,
        string Updateddescription, decimal Updatedprice)
    {
        var updatedItem = new MenuItem
        {
            ItemId = 1, 
            RestaurantId = 1,
            Name = Updatedname,
            Description = Updateddescription,
            Price = Updatedprice
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
        await AddMenuItemAsync(
            restaurantId: 2,
            name: "Grilled Chicken",
            description: "Delicious grilled chicken served with vegetables.", 
            price: 12.99m);

        await UpdateMenuItemAsync(
            itemId: 1,
            UpdatedrestaurantId: 4, "Grilled Chicken (Updated)",
            Updateddescription: "Updated description for grilled chicken.",
            Updatedprice: 14.99m);

        await GetAllMenuItemsAsync();
        await DeleteMenuItemAsync();
    }
}
