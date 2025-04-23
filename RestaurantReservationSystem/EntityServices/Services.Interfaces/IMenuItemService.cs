namespace RestaurantReservation.Db.Services.Interfaces
{
    public interface IMenuItemService
    {
        Task AddMenuItemAsync();
        Task UpdateMenuItemAsync();
        Task GetAllMenuItemsAsync();
        Task DeleteMenuItemAsync();
        Task ExecuteExamplesAsync();
    }
}
