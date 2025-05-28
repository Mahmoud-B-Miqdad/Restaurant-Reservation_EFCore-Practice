using AutoMapper;
using RestaurantReservationSystem.Domain.DTOs.Requests;
using RestaurantReservationSystem.Domain.DTOs.Responses;
using RestaurantReservationSystem.Domain.Exceptions;
using RestaurantReservationSystem.Domain.Interfaces.Repositories;
using RestaurantReservationSystem.Domain.Interfaces.Services;
using RestaurantReservationSystem.Domain.Models;

namespace RestaurantReservationSystem.Domain.Services
{
    /// <summary>
    /// Provides business logic and service methods for managing reservation operations.
    /// </summary>
    public class ReservationService : IReservationService
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IMenuItemRepository _menuItemRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly ITableRepository _tableRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReservationService"/> class.
        /// </summary>
        /// <param name="repository">The repository responsible for reservation data access.</param>
        /// <param name="mapper">The mapper used to convert between entities and DTOs.</param>
        public ReservationService(IReservationRepository repository, IMapper mapper, IOrderRepository orderRepository,
            IMenuItemRepository menuItemRepository, ICustomerRepository customerRepository,
            IRestaurantRepository restaurantRepository, ITableRepository tableRepository)
        {
            _reservationRepository = repository;
            _orderRepository = orderRepository;
            _mapper = mapper;
            _menuItemRepository = menuItemRepository;
            _customerRepository = customerRepository;
            _restaurantRepository = restaurantRepository;
            _tableRepository = tableRepository;
        }

        private async Task<ReservationModel> EnsureReservationExistsAsync(int reservationId)

        {
            var reservation = await _reservationRepository.GetByIdAsync(reservationId);
            if (reservation == null)
                throw new NotFoundException($"Reservation with ID {reservationId} not found");

            return reservation;
        }

        /// <inheritdoc />
        public async Task<List<ReservationResponse>> GetAllAsync()
        {
            var reservations = await _reservationRepository.GetAllAsync();
            return _mapper.Map<List<ReservationResponse>>(reservations);
        }

        /// <inheritdoc />
        public async Task<ReservationResponse?> GetByIdAsync(int id)
        {
            var reservation = await EnsureReservationExistsAsync(id);
            return _mapper.Map<ReservationResponse>(reservation);
        }

        /// <inheritdoc />
        public async Task<ReservationResponse> CreateAsync(ReservationRequest request)
        {
            var reservation = _mapper.Map<ReservationModel>(request);
            await _reservationRepository.AddAsync(reservation);
            return _mapper.Map<ReservationResponse>(reservation);
        }

        /// <inheritdoc />
        public async Task<ReservationResponse?> UpdateAsync(int id, ReservationRequest request)
        {
            var updatedReservation = await EnsureReservationExistsAsync(id);

            _mapper.Map(request, updatedReservation);
            await _reservationRepository.UpdateAsync(updatedReservation);
            return _mapper.Map<ReservationResponse>(updatedReservation);
        }

        /// <inheritdoc />
        public async Task<bool> DeleteAsync(int id)
        {
            await EnsureReservationExistsAsync(id);

            var existing = await _reservationRepository.GetReservationByIdWithOrdersAsync(id);
            if (existing == null) return false;

            if (existing.Orders.Any())
                throw new InvalidOperationException("Cannot delete reservation with existing orders.");

            await _reservationRepository.DeleteAsync(id);
            return true;
        }

        /// <inheritdoc />
        public async Task<CustomerResponse?> GetCustomerAsync(int reservationId)
        {
            var reservation = await EnsureReservationExistsAsync(reservationId);
            var customer = await _customerRepository.GetCustomerByReservationIdAsync(reservationId);
            return customer == null ? null : _mapper.Map<CustomerResponse>(customer);
        }

        /// <inheritdoc />
        public async Task<List<ReservationResponse>> GetReservationsByCustomerAsync(int customerId)
        {
            var customer = await _customerRepository.GetByIdAsync(customerId);
            if (customer == null)
                throw new NotFoundException($"Customer with ID {customerId} not found");

            var reservationsByCustomer = await _reservationRepository.GetReservationsByCustomerAsync(customerId);
            return _mapper.Map<List<ReservationResponse>>(reservationsByCustomer);
        }

        /// <inheritdoc />
        public async Task<ReservationResponse?> GetReservationByOrderIdAsync(int orderId)
        {
            var order = await _orderRepository.GetByIdAsync(orderId);
            if (order == null)
                throw new NotFoundException($"Order with ID {orderId} not found");

            var reservation = await _reservationRepository.GetReservationByOrderIdAsync(orderId);
            return reservation == null ? null : _mapper.Map<ReservationResponse>(reservation);
        }


        /// <inheritdoc />
        public async Task<List<ReservationResponse>> GetReservationsByRestaurantIdAsync(int restaurantId)
        {
            var restaurant = await _restaurantRepository.GetByIdAsync(restaurantId);
            if (restaurant == null)
                throw new NotFoundException($"Restaurant with ID {restaurantId} not found");

            var reservation = await _reservationRepository.GetReservationsByRestaurantIdAsync(restaurantId);
            return _mapper.Map<List<ReservationResponse>>(reservation);
        }

        /// <inheritdoc />
        public async Task<List<ReservationResponse>> GetReservationsByTableIdAsync(int tableId)
        {
            var table = await _tableRepository.GetByIdAsync(tableId);
            if (table == null)
                throw new NotFoundException($"Table with ID {tableId} not found");

            var orders = await _reservationRepository.GetReservationsByTableIdAsync(tableId);
            return _mapper.Map<List<ReservationResponse>>(orders);
        }
    }
}