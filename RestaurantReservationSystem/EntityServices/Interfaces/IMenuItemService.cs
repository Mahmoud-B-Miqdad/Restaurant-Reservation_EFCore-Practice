using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Db.Services.Interfaces
{
    public interface IMenuItemService
    {
        Task AddMenuItemAsync(int restaurantId, string name, string description, decimal price);
        Task UpdateMenuItemAsync(int itemId, int UpdatedrestaurantId, string Updatedname,
        string Updateddescription, decimal Updatedprice);
        Task<IEnumerable<MenuItem>> GetAllMenuItemsAsync();
        Task DeleteMenuItemAsync(int menueItemIdToDelete);
        Task ExecuteExamplesAsync();
    }
}
