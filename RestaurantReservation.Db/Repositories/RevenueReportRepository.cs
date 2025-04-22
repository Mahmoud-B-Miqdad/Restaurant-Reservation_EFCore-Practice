using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace RestaurantReservation.Db.Repositories;

public class RevenueReportRepository
{
    private readonly RestaurantReservationDbContext _context;

    public RevenueReportRepository(RestaurantReservationDbContext context)
    {
        _context = context;
    }

    public async Task<decimal> GetTotalRevenueByRestaurantAsync(int restaurantId)
    {
        var param = new SqlParameter("@restaurantId", restaurantId);

        var result = await _context
            .RevenueResults
            .FromSqlRaw("SELECT dbo.CalculateTotalRevenue(@restaurantId) AS TotalRevenue", param)
            .AsNoTracking()
            .FirstOrDefaultAsync();

        return result?.TotalRevenue ?? 0;
    }
}
