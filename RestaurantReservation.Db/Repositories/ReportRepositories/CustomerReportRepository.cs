using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace RestaurantReservation.Db.Repositories.ReportRepositories;

internal class CustomerReportRepository : ICustomerReportRepository
{
    private readonly RestaurantReservationDbContext _context;

    public CustomerReportRepository(RestaurantReservationDbContext context)
    {
        _context = context;
    }

    public async Task<List<CustomerView>> GetCustomersByPartySizeAsync(int minPartySize)
    {
        var param = new SqlParameter("@minPartySize", minPartySize);
        return await _context
            .CustomerViews
            .FromSqlRaw("EXEC GetCustomersByPartySize @minPartySize", param)
            .AsNoTracking()
            .ToListAsync();
    }
}
