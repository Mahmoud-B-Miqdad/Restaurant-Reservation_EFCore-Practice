using Microsoft.EntityFrameworkCore;

namespace RestaurantReservation.Db.Repositories.ReportRepositories;

internal class EmployeeReportRepository : IEmployeeReportRepository
{
    private readonly RestaurantReservationDbContext _context;

    public EmployeeReportRepository(RestaurantReservationDbContext context)
    {
        _context = context;
    }

    public async Task<List<EmployeeRestaurantDetails>> GetEmployeesAsync()
    {
        return await _context.Set<EmployeeRestaurantDetails>().ToListAsync();
    }
}
