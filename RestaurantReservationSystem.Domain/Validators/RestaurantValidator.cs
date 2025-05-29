using RestaurantReservationSystem.Domain.Exceptions;
using RestaurantReservationSystem.Domain.Interfaces.Repositories;
using RestaurantReservationSystem.Domain.Models;

namespace RestaurantReservationSystem.Domain.Validators
{
    public class RestaurantValidator
    {
        private readonly IRestaurantRepository _restaurantRepository;

        public RestaurantValidator(IRestaurantRepository restaurantRepository)
        {
            _restaurantRepository = restaurantRepository;
        }

        public async Task<RestaurantModel> EnsureRestaurantExistsAsync(int restaurantId)
        {
            var restaurant = await _restaurantRepository.GetByIdAsync(restaurantId);
            if (restaurant == null)
                throw new NotFoundException($"Restaurant with ID {restaurantId} not found");

            return restaurant;
        }
    }

}