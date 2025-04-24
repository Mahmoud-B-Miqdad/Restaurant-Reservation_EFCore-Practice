using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Db.Repositories.Interfaces;

public interface IReservationRepository
{
    Task<List<Reservation>> GetAllAsync();
    Task AddAsync(Reservation reservation);
    Task UpdateAsync(Reservation reservation);
    Task DeleteAsync(int id);
}
