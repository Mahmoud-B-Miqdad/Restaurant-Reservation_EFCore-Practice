using Microsoft.EntityFrameworkCore;
using RestaurantReservation.Db.Models;
using RestaurantReservation.Db.Repositories.Interfaces;
using RestaurantReservation.Db.Services.Interfaces;
using RestaurantReservationSystem.Constants;

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
            throw new InvalidOperationException(DefaultErrorMessages.AddFailed, ex);
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
            throw new InvalidOperationException(DefaultErrorMessages.RetrieveFailed, ex);
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
            throw new InvalidOperationException(DefaultErrorMessages.UpdateFailed, ex);
        }
    }

    public async Task DeleteMenuItemAsync()
    {
        int itemIdToDelete = 1; 

        try
        {
            await _menuItemRepository.DeleteAsync(itemIdToDelete);
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
        await AddMenuItemAsync(
            restaurantId: DefaultTestValues.Id2,
            name: DefaultTestValues.DefaultMenuItemName,
            description: DefaultTestValues.DefaultMenuItemDescription,
            price: DefaultTestValues.DefaultMenuItemPrice);

        await UpdateMenuItemAsync(
            itemId: DefaultTestValues.Id1,
            UpdatedrestaurantId: DefaultTestValues.Id4,
            DefaultTestValues.UpdatedMenuItemName,
            Updateddescription: DefaultTestValues.UpdatedMenuItemDescription,
            Updatedprice: DefaultTestValues.UpdatedMenuItemPrice);

        await GetAllMenuItemsAsync();
        await DeleteMenuItemAsync();
    }
}
