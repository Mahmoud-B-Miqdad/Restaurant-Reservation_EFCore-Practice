namespace RestaurantReservation.Db.Services.Interfaces
{
    public interface IReservationService
    {
        Task AddReservationAsync(int customerId, int restaurantId, int tableId, DateTime reservationDate, int partySize);
        Task UpdateReservationAsync(int reservationId, int updatedCustomerId, int updatedRestaurantId,
        int updatedTableId, DateTime updatedReservationDate, int updatedPartySize);
        Task GetAllReservationsAsync();
        Task DeleteReservationAsync();
        Task ExecuteExamplesAsync();
    }
}