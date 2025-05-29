using RestaurantReservationSystem.Domain.Exceptions;
using RestaurantReservationSystem.Domain.Interfaces.Repositories;
using RestaurantReservationSystem.Domain.Models;

namespace RestaurantReservationSystem.Domain.Validators
{
    public class ReservationValidator
    {
        private readonly IReservationRepository _reservationRepository;
        public ReservationValidator(IReservationRepository reservationRepository)
        {
            _reservationRepository = reservationRepository;
        }

        public async Task<ReservationModel> EnsureRestaurantExistsAsync(int reservationId)
        {
            var reservation = await _reservationRepository.GetByIdAsync(reservationId);
            if (reservation == null)
                throw new NotFoundException($"Reservation with ID {reservationId} not found");

            return reservation;
        }
    }
}