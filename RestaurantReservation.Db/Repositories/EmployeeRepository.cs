using Microsoft.EntityFrameworkCore;
using RestaurantReservation.Db.Constants;
using RestaurantReservation.Db.Entities;
using RestaurantReservation.Db.Repositories.Interfaces;

namespace RestaurantReservation.Db.Repositories
{
    internal class EmployeeRepository : IEmployeeRepository
    {
        private readonly RestaurantReservationDbContext _context;

        public EmployeeRepository(RestaurantReservationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Employee>> ListManagersAsync()
        {
            return await _context.Employees
                                 .Where(e => e.Position == EmployeePositions.Manager)
                                 .ToListAsync();
        }

        public async Task<List<Employee>> GetAllAsync() => await _context.Employees.ToListAsync();

        public async Task<Employee> GetByIdAsync(int id)
        {
            return await _context.Employees.FindAsync(id);
        }

        public async Task AddAsync(Employee employee)
        {
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Employee employee)
        {
            var existingEmployee = await _context.Employees.FindAsync(employee.EmployeeId);
            if (existingEmployee == null)
                return;

            _context.Entry(existingEmployee).CurrentValues.SetValues(employee);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee is null) return;

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Employee>> GetByRestaurantIdAsync(int restaurantId)
        {
            return await _context.Employees
                .Where(e => e.RestaurantId == restaurantId)
                .ToListAsync();
        }

        public async Task<Restaurant?> GetRestaurantByEmployeeIdAsync(int employeeId)
        {
            var employee = await _context.Employees
                .Include(e => e.Restaurant)
                .FirstOrDefaultAsync(e => e.EmployeeId == employeeId);

            return employee?.Restaurant;
        }
    }
}