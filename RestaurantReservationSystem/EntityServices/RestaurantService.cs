using RestaurantReservation.Db.Models;

public class RestaurantService
{
    private readonly RestaurantOperations _restaurantOperations;

    public RestaurantService(RestaurantOperations restaurantOperations)
    {
        _restaurantOperations = restaurantOperations;
    }

    public async Task AddRestaurantAsync()
    {
        var restaurant = new Restaurant
        {
            Name = "Test Restaurant",
            Address = "Gaza",
            PhoneNumber = "0590000000",
            OpeningHours = "8:00 - 16:00"
        };

        await _restaurantOperations.AddAsync(restaurant);
    }

    public async Task<List<Restaurant>> GetAllRestaurantsAsync()
    {
        return await _restaurantOperations.GetAllAsync();
    }

    public async Task UpdateRestaurantAsync()
    {
        var restaurant = new Restaurant
        {
            RestaurantId = 1,
            Name = "Updated Name",
            Address = "Updated Address",
            PhoneNumber = "0599999999",
            OpeningHours = "9:00 - 17:00"
        };

        await _restaurantOperations.UpdateAsync(restaurant);
    }

    public async Task DeleteRestaurantAsync()
    {
        int restaurantIdToDelete = 2;
        await _restaurantOperations.DeleteAsync(restaurantIdToDelete);
    }

    public async Task ExecuteExamplesAsync()
    {
        await AddRestaurantAsync();

        await UpdateRestaurantAsync();

        await DeleteRestaurantAsync();

        var all = await GetAllRestaurantsAsync();
        foreach (var restaurant in all)
        {
            Console.WriteLine($"[Restaurant] {restaurant.RestaurantId} - {restaurant.Name}, {restaurant.Address}");
        }
    }
}
