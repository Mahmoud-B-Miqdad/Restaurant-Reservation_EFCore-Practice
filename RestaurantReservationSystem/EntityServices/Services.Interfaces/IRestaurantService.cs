namespace RestaurantReservation.Db.Services.Interfaces
{
    public interface IRestaurantService
    {
        Task AddRestaurantAsync();
        Task UpdateRestaurantAsync();
        Task GetAllRestaurantsAsync();
        Task DeleteRestaurantAsync();
        Task ExecuteExamplesAsync();
    }
}