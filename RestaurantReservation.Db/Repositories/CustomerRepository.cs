using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RestaurantReservation.Db;
using RestaurantReservation.Db.Entities;
using RestaurantReservationSystem.Domain.Interfaces.Repositories;
using RestaurantReservationSystem.Domain.Models;
internal class CustomerRepository : ICustomerRepository
{
    private readonly RestaurantReservationDbContext _context;
    private readonly IMapper _mapper;

    public CustomerRepository(RestaurantReservationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<CustomerModel>> GetAllAsync()
    {
        var customers = await _context.Customers.ToListAsync();
        return _mapper.Map<List<CustomerModel>>(customers);
    }

    public async Task<CustomerModel> GetByIdAsync(int id)
    {
        var customer = await _context.Customers.FindAsync(id);
        return _mapper.Map<CustomerModel>(customer);
    }


    public async Task AddAsync(CustomerModel model)
    {
        var customer = _mapper.Map<Customer>(model);
        await _context.Customers.AddAsync(customer);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(CustomerModel customer)
    {
        var existingCustomer = await _context.Customers.FindAsync(customer.CustomerId);
        if (existingCustomer == null)
            return;

        _mapper.Map(customer, existingCustomer);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var customer = await _context.Customers.FindAsync(id);
        if (customer != null)
        {
            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
        }
    }
}