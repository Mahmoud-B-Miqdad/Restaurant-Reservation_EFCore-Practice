namespace RestaurantReservation.Db.Repositories.ReportRepositories;

public interface IReservationReportRepository
{
    Task<List<ReservationDetailsView>> GetReservationsAsync();
}
