using RestaurantReservation.Db.Models;
using Microsoft.EntityFrameworkCore;
using RestaurantReservation.Db.Services.Interfaces;
using RestaurantReservation.Db.Repositories.Interfaces;

public class RestaurantService : IRestaurantService
{
    private readonly IRestaurantRepository _restaurantRepository;

    public RestaurantService(IRestaurantRepository restaurantRepository)
    {
        _restaurantRepository = restaurantRepository;
    }

    public async Task AddRestaurantAsync(string name, string address, string phoneNumber, string openingHours)
    {
        var restaurant = new Restaurant
        {
            Name = name,
            Address = address,
            PhoneNumber = phoneNumber,
            OpeningHours = openingHours
        };

        try
        {
            await _restaurantRepository.AddAsync(restaurant);
        }
        catch (DbUpdateException ex)
        {
            throw new InvalidOperationException("Failed to add the restaurant. Ensure all required fields are valid.", ex);
        }
    }

    public async Task GetAllRestaurantsAsync()
    {
        try
        {
            var all = await _restaurantRepository.GetAllAsync();
            foreach (var restaurant in all)
            {
                Console.WriteLine($"[Restaurant] {restaurant.RestaurantId} - {restaurant.Name}, {restaurant.Address}");
            }
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Failed to retrieve restaurants from the database.", ex);
        }
    }

    public async Task UpdateRestaurantAsync(int restaurantId, string updatedName, string updatedAddress,
        string updatedPhoneNumber, string updatedOpeningHours)
    {
        var restaurant = new Restaurant
        {
            RestaurantId = restaurantId,
            Name = updatedName,
            Address = updatedAddress,
            PhoneNumber = updatedPhoneNumber,
            OpeningHours = updatedOpeningHours
        };

        try
        {
            await _restaurantRepository.UpdateAsync(restaurant);
        }
        catch (DbUpdateConcurrencyException ex)
        {
            throw new InvalidOperationException("Failed to update the restaurant. It may have been modified or deleted by another process.", ex);
        }
    }

    public async Task DeleteRestaurantAsync()
    {
        int restaurantIdToDelete = 2;

        try
        {
            await _restaurantRepository.DeleteAsync(restaurantIdToDelete);
        }
        catch (DbUpdateException ex)
        {
            throw new InvalidOperationException("Cannot delete the restaurant because it has related data (e.g., employees or reservations).", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Unexpected error occurred while deleting the restaurant.", ex);
        }
    }

    public async Task ExecuteExamplesAsync()
    {
        await AddRestaurantAsync(
            name: "TestRestaurant",
            address: "TestAddress",
            phoneNumber: "0590000000",
            openingHours: "8:00 - 16:00");

        await UpdateRestaurantAsync(
            restaurantId: 1,
            updatedName: "UpdatedRestaurant",
            updatedAddress: "UpdatedAddress",
            updatedPhoneNumber: "0599999999",
            updatedOpeningHours: "9:00 - 17:00");

        await GetAllRestaurantsAsync();
        await DeleteRestaurantAsync();
    }
}