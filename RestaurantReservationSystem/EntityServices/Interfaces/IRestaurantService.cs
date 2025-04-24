using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Db.Services.Interfaces
{
    public interface IRestaurantService
    {
        Task AddRestaurantAsync(string name, string address, string phoneNumber, string openingHours);
        Task UpdateRestaurantAsync(int restaurantId, string updatedName, string updatedAddress,
        string updatedPhoneNumber, string updatedOpeningHours);
        Task<IEnumerable<Restaurant>> GetAllRestaurantsAsync();
        Task DeleteRestaurantAsync();
        Task ExecuteExamplesAsync();
    }
}