using RestaurantReservationSystem.Domain.Models;

namespace RestaurantReservationSystem.Domain.Interfaces.Repositories;

public interface IReservationRepository
{
    Task<List<ReservationModel>> GetAllAsync();
    Task<ReservationModel> GetByIdAsync(int id);
    Task AddAsync(ReservationModel reservation);
    Task UpdateAsync(ReservationModel reservation);
    Task DeleteAsync(int id);
    Task<IEnumerable<ReservationModel>> GetReservationsByRestaurantIdAsync(int restaurantId);
    Task<IEnumerable<ReservationModel>> GetReservationsByTableIdAsync(int tableId);
    Task<ReservationModel?> GetReservationByOrderIdAsync(int orderId);
    Task<List<ReservationModel>> GetReservationsByCustomerAsync(int customerId);
}