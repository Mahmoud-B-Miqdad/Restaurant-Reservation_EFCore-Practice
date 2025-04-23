namespace RestaurantReservation.Db.Repositories.ReportRepositories;

public interface IEmployeeReportRepository
{
    Task<List<EmployeeRestaurantDetails>> GetEmployeesAsync();
}
