using AutoMapper;
using RestaurantReservation.Db.Entities;
using RestaurantReservation.Db.Repositories.Interfaces;
using RestaurantReservationSystem.API.DTOs.Requests;
using RestaurantReservationSystem.API.DTOs.Responses;
using RestaurantReservationSystem.API.Services.Interfaces;

namespace RestaurantReservationSystem.API.Services
{
    /// <summary>
    /// Provides business logic and service methods for managing customer operations.
    /// </summary>
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IReservationRepository _reservationRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerService"/> class.
        /// </summary>
        /// <param name="repository">The repository responsible for customer data access.</param>
        /// <param name="mapper">The mapper used to convert between entities and DTOs.</param>
        public CustomerService(ICustomerRepository repository, IMapper mapper, IReservationRepository reservationRepository)
        {
            _customerRepository = repository;
            _mapper = mapper;
            _reservationRepository = reservationRepository;
        }

        /// <inheritdoc />
        public async Task<List<CustomerResponse>> GetAllAsync()
        {
            var customer = await _customerRepository.GetAllAsync();
            return _mapper.Map<List<CustomerResponse>>(customer);
        }

        /// <inheritdoc />
        public async Task<CustomerResponse?> GetByIdAsync(int id)
        {
            var customer = await _customerRepository.GetByIdAsync(id);
            return customer == null ? null : _mapper.Map<CustomerResponse>(customer);
        }

        /// <inheritdoc />
        public async Task<CustomerResponse> CreateAsync(CustomerRequest request)
        {
            var customer = _mapper.Map<Customer>(request);
            await _customerRepository.AddAsync(customer);
            return _mapper.Map<CustomerResponse>(customer);
        }

        /// <inheritdoc />
        public async Task<CustomerResponse?> UpdateAsync(int id, CustomerRequest request)
        {
            var updatedCustomer = await _customerRepository.GetByIdAsync(id);
            if (updatedCustomer == null) return null;

            _mapper.Map(request, updatedCustomer);
            await _customerRepository.UpdateAsync(updatedCustomer);
            return _mapper.Map<CustomerResponse>(updatedCustomer);
        }

        /// <inheritdoc />
        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _customerRepository.GetByIdAsync(id);
            if (existing == null) return false;

            await _customerRepository.DeleteAsync(id);
            return true;
        }

        /// <inheritdoc />
        public async Task<IEnumerable<ReservationResponse>> GetReservationAsync(int customerId)
        {
            var reservations = await _reservationRepository.GetReservationsByCustomerAsync(customerId);
            return _mapper.Map<IEnumerable<ReservationResponse>>(reservations);
        }
    }
}