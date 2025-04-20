using RestaurantReservation.Db.Models;

public class ReservationService
{
    private readonly ReservationOperations _reservationOperations;

    public ReservationService(ReservationOperations reservationOperations)
    {
        _reservationOperations = reservationOperations;
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
            await _reservationOperations.AddAsync(reservation);
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
            var all = await _reservationOperations.GetAllAsync();
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
            await _reservationOperations.UpdateAsync(reservation);
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
            await _reservationOperations.DeleteAsync(reservationIdToDelete);
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
