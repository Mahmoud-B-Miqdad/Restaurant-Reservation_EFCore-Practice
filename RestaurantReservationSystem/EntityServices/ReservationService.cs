using RestaurantReservation.Db.Models;
using RestaurantReservation.Db.Repositories;
using RestaurantReservation.Db.Repositories.Interfaces;
using RestaurantReservation.Db.Services.Interfaces;
public class ReservationService : IReservationService
{
    private readonly IReservationRepository _reservationRepository;

    public ReservationService(IReservationRepository reservationRepository)
    {
        _reservationRepository = reservationRepository;
    }

    public async Task AddReservationAsync()
    {
        var reservation = new Reservation
        {
            CustomerId = 1, 
            RestaurantId = 1, 
            TableId = 1, 
            ReservationDate = DateTime.Now.AddDays(1), 
            PartySize = 4 
        };

        try
        {
            await _reservationRepository.AddAsync(reservation);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Failed to add the reservation.", ex);
        }
    }

    public async Task GetAllReservationsAsync()
    {
        try
        {
            var all = await _reservationRepository.GetAllAsync();
            foreach (var reservation in all)
            {
                Console.WriteLine($"[Reservation] {reservation.ReservationId} - CustomerId: {reservation.CustomerId}, " +
                    $"RestaurantId: {reservation.RestaurantId}, TableId: {reservation.TableId}, " +
                    $"ReservationDate: {reservation.ReservationDate}, PartySize: {reservation.PartySize}");
            }
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Failed to retrieve reservations from the database.", ex);
        }
    }

    public async Task UpdateReservationAsync()
    {
        var reservation = new Reservation
        {
            ReservationId = 3, 
            CustomerId = 4, 
            RestaurantId = 4, 
            TableId = 4, 
            ReservationDate = DateTime.Now.AddDays(2),
            PartySize = 10 
        };

        try
        {
            await _reservationRepository.UpdateAsync(reservation);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Failed to update the reservation.", ex);
        }
    }

    public async Task DeleteReservationAsync()
    {
        int reservationIdToDelete = 1; 

        try
        {
            await _reservationRepository.DeleteAsync(reservationIdToDelete);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Failed to delete the reservation.", ex);
        }
    }

    public async Task ExecuteExamplesAsync()
    {
        await AddReservationAsync();
        await UpdateReservationAsync();
        await GetAllReservationsAsync();
        await DeleteReservationAsync();
    }
}
