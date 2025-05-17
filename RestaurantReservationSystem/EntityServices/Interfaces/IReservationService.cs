using RestaurantReservation.Db.Entities;

namespace RestaurantReservation.Db.Services.Interfaces
{
    public interface IReservationService
    {
        Task AddReservationAsync(int customerId, int restaurantId, int tableId, DateTime reservationDate, int partySize);
        Task UpdateReservationAsync(int reservationId, int updatedCustomerId, int updatedRestaurantId,
        int updatedTableId, DateTime updatedReservationDate, int updatedPartySize);
        Task<IEnumerable<Reservation>> GetAllReservationsAsync();
        Task DeleteReservationAsync(int reservationIdToDelete);
        Task ExecuteExamplesAsync();
    }
}