using RestaurantReservation.Db.Models;
using RestaurantReservation.Db.Repositories.Interfaces;
using RestaurantReservation.Db.Services.Interfaces;
public class ReservationService : IReservationService
{
    private readonly IReservationRepository _reservationRepository;

    public ReservationService(IReservationRepository reservationRepository)
    {
        _reservationRepository = reservationRepository;
    }

    public async Task AddReservationAsync(int customerId, int restaurantId, int tableId, DateTime reservationDate, int partySize)
    {
        var reservation = new Reservation
        {
            CustomerId = customerId,
            RestaurantId = restaurantId,
            TableId = tableId,
            ReservationDate = reservationDate,
            PartySize = partySize
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

    public async Task UpdateReservationAsync(int reservationId, int updatedCustomerId, int updatedRestaurantId, 
        int updatedTableId, DateTime updatedReservationDate, int updatedPartySize)
    {
        var reservation = new Reservation
        {
            ReservationId = reservationId,
            CustomerId = updatedCustomerId,
            RestaurantId = updatedRestaurantId,
            TableId = updatedTableId,
            ReservationDate = updatedReservationDate,
            PartySize = updatedPartySize
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
        await AddReservationAsync(
            customerId: 1,
            restaurantId: 1,
            tableId: 1,
            reservationDate: DateTime.Now.AddDays(1),
            partySize: 4);

        await UpdateReservationAsync(
            reservationId: 3,
            updatedCustomerId: 4,
            updatedRestaurantId: 4,
            updatedTableId: 4,
            updatedReservationDate: DateTime.Now.AddDays(2),
            updatedPartySize: 10);

        await GetAllReservationsAsync();
        await DeleteReservationAsync();
    }
}
