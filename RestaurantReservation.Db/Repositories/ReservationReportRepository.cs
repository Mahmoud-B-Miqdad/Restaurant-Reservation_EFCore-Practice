
using Microsoft.EntityFrameworkCore;
using RestaurantReservation.Db.Models;
namespace RestaurantReservation.Db.Repositories;

public class ReservationReportRepository
{
    private readonly RestaurantReservationDbContext _context;

    public ReservationReportRepository(RestaurantReservationDbContext context)
    {
        _context = context;
    }

    public async Task<List<ReservationDetailsView>> GetReservationsAsync()
    {
        return await _context.Set<ReservationDetailsView>().ToListAsync();
    }
}
