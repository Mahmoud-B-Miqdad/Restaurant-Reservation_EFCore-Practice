using RestaurantReservation.Db.Entities;

namespace RestaurantReservationSystem.Domain.Interfaces.Repositories;

public interface IReservationRepository
{
    Task<List<Reservation>> GetAllAsync();
    Task<Reservation> GetByIdAsync(int id);
    Task AddAsync(Reservation reservation);
    Task UpdateAsync(Reservation reservation);
    Task DeleteAsync(int id);
    Task<IEnumerable<Reservation>> GetByRestaurantIdAsync(int restaurantId);
    Task<IEnumerable<Reservation>> GetReservationsByTableIdAsync(int tableId);
}
