using RestaurantReservation.Db.Models;
using Microsoft.EntityFrameworkCore;
using RestaurantReservation.Db.Services.Interfaces;
using RestaurantReservation.Db.Repositories.Interfaces;
using RestaurantReservationSystem.Constants;

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
            throw new InvalidOperationException(DefaultErrorMessages.AddFailed, ex);
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
            throw new InvalidOperationException(DefaultErrorMessages.RetrieveFailed, ex);
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
            throw new InvalidOperationException(DefaultErrorMessages.UpdateFailed, ex);
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
            throw new InvalidOperationException(DefaultErrorMessages.DeleteWithRelations, ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException(DefaultErrorMessages.DeleteUnexpected, ex);
        }
    }

    public async Task ExecuteExamplesAsync()
    {
        await AddRestaurantAsync(
            name: DefaultTestValues.DefaultRestaurantName,
            address: DefaultTestValues.DefaultRestaurantAddress,
            phoneNumber: DefaultTestValues.DefaultPhoneNumber,
            openingHours: DefaultTestValues.DefaultOpeningHours);

        await UpdateRestaurantAsync(
            restaurantId: DefaultTestValues.Id1,
            updatedName: DefaultTestValues.UpdatedRestaurantName,
            updatedAddress: DefaultTestValues.UpdatedRestaurantAddress,
            updatedPhoneNumber: DefaultTestValues.UpdatedPhoneNumber,
            updatedOpeningHours: DefaultTestValues.UpdatedOpeningHours);

        await GetAllRestaurantsAsync();
        await DeleteRestaurantAsync();
    }
}