namespace RestaurantReservation.Db.Services.Interfaces
{
    public interface IReservationService
    {
        Task AddReservationAsync();
        Task UpdateReservationAsync();
        Task GetAllReservationsAsync();
        Task DeleteReservationAsync();
        Task ExecuteExamplesAsync();
    }
}