namespace RestaurantReservation.Db.Repositories.ReportRepositories;

public interface ICustomerReportRepository
{
    Task<List<CustomerView>> GetCustomersByPartySizeAsync(int minPartySize);
}
