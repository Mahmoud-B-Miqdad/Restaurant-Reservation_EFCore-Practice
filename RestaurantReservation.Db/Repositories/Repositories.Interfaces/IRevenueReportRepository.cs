namespace RestaurantReservation.Db.Repositories.ReportRepositories;

public interface IRevenueReportRepository
{
    Task<decimal> GetTotalRevenueByRestaurantAsync(int restaurantId);
}
