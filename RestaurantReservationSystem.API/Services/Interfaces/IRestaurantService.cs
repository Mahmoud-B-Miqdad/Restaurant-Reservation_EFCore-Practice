using RestaurantReservationSystem.API.DTOs;


namespace RestaurantReservationSystem.API.Services.Interfaces
{
    public interface IRestaurantService
    {
        Task<IEnumerable<RestaurantResponse>> GetAllAsync();
        Task<RestaurantResponse> GetByIdAsync(int id);
        Task<RestaurantResponse> CreateAsync(RestaurantRequest request);
        Task<bool> UpdateAsync(int id, RestaurantRequest request);
        Task<bool> DeleteAsync(int id);
    }
}