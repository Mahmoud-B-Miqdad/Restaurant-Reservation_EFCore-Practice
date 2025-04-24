using Microsoft.EntityFrameworkCore;
using RestaurantReservation.Db.Models;
using RestaurantReservation.Db.Repositories.Interfaces;
using RestaurantReservation.Db.Services.Interfaces;
using RestaurantReservationSystem.Constants;
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
            throw new InvalidOperationException(DefaultErrorMessages.AddFailed, ex);
        }
    }

    public async Task<IEnumerable<Reservation>> GetAllReservationsAsync()
    {
        try
        {
            var all = await _reservationRepository.GetAllAsync();
            return all;
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException(DefaultErrorMessages.RetrieveFailed, ex);
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
            throw new InvalidOperationException(DefaultErrorMessages.UpdateFailed, ex);
        }
    }

    public async Task DeleteReservationAsync()
    {
        int reservationIdToDelete = 1; 

        try
        {
            await _reservationRepository.DeleteAsync(reservationIdToDelete);
        }
        catch (DbUpdateException ex)
        {
            throw new InvalidOperationException(DefaultErrorMessages.DeleteWithRelations, ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException(DefaultErrorMessages.DeleteUnexpected, ex);
        }
    }

    public async Task ExecuteExamplesAsync()
    {
        await AddReservationAsync(
            customerId: DefaultTestValues.Id1,
            restaurantId: DefaultTestValues.Id1,
            tableId: DefaultTestValues.Id1,
            reservationDate: DefaultTestValues.ReservationDateTomorrow,
            partySize: DefaultTestValues.DefaultPartySize);

        await UpdateReservationAsync(
            reservationId: DefaultTestValues.Id4,
            updatedCustomerId: DefaultTestValues.Id4,
            updatedRestaurantId: DefaultTestValues.Id4,
            updatedTableId: DefaultTestValues.Id4,
            updatedReservationDate: DefaultTestValues.ReservationDateAfterTwoDays,
            updatedPartySize: DefaultTestValues.UpdatedPartySize);

        var reservations = await GetAllReservationsAsync();
        foreach (var reservation in reservations)
        {
            Console.WriteLine($"[Reservation] {reservation.ReservationId} - CustomerId: {reservation.CustomerId}, " +
                $"RestaurantId: {reservation.RestaurantId}, TableId: {reservation.TableId}, " +
                $"ReservationDate: {reservation.ReservationDate}, PartySize: {reservation.PartySize}");
        }

        await DeleteReservationAsync();
    }
}
