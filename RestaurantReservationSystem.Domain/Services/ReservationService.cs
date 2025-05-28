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
        private readonly IOrderService _orderService;
        private readonly IMenuItemService _menuItemService;
        private readonly ICustomerService _customerService;
        private readonly IRestaurantService _restaurantService;
        private readonly ITableService _tableService;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReservationService"/> class.
        /// </summary>
        /// <param name="repository">The repository responsible for reservation data access.</param>
        /// <param name="mapper">The mapper used to convert between entities and DTOs.</param>
        public ReservationService(IReservationRepository repository, IMapper mapper, IOrderService orderService,
            IMenuItemService menuItemService, ICustomerService customerService,
            IRestaurantService restaurantService, ITableService tableService)
        {
            _reservationRepository = repository;
            _orderService = orderService;
            _mapper = mapper;
            _menuItemService = menuItemService;
            _customerService = customerService;
            _restaurantService = restaurantService;
            _tableService = tableService;
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
        public async Task<List<ReservationResponse>> GetReservationsByCustomerIdAsync(int customerId)
        {
            var customer = await _customerService.GetByIdAsync(customerId);
            if (customer == null)
                throw new NotFoundException($"Customer with ID {customerId} not found");

            var reservationsByCustomer = await _reservationRepository.GetReservationsByCustomerIdAsync(customerId);
            return _mapper.Map<List<ReservationResponse>>(reservationsByCustomer);
        }

        /// <inheritdoc />
        public async Task<ReservationResponse?> GetReservationByOrderIdAsync(int orderId)
        {
            var order = await _orderService.GetByIdAsync(orderId);
            if (order == null)
                throw new NotFoundException($"Order with ID {orderId} not found");

            var reservation = await _reservationRepository.GetReservationByOrderIdAsync(orderId);
            return reservation == null ? null : _mapper.Map<ReservationResponse>(reservation);
        }

        /// <inheritdoc />
        public async Task<List<ReservationResponse>> GetReservationsByRestaurantIdAsync(int restaurantId)
        {
            var restaurant = await _restaurantService.GetByIdAsync(restaurantId);
            if (restaurant == null)
                throw new NotFoundException($"Restaurant with ID {restaurantId} not found");

            var reservation = await _reservationRepository.GetReservationsByRestaurantIdAsync(restaurantId);
            return _mapper.Map<List<ReservationResponse>>(reservation);
        }

        /// <inheritdoc />
        public async Task<List<ReservationResponse>> GetReservationsByTableIdAsync(int tableId)
        {
            var table = await _tableService.GetByIdAsync(tableId);
            if (table == null)
                throw new NotFoundException($"Table with ID {tableId} not found");

            var orders = await _reservationRepository.GetReservationsByTableIdAsync(tableId);
            return _mapper.Map<List<ReservationResponse>>(orders);
        }

        private async Task<ReservationModel> EnsureReservationExistsAsync(int reservationId)

        {
            var reservation = await _reservationRepository.GetByIdAsync(reservationId);
            if (reservation == null)
                throw new NotFoundException($"Reservation with ID {reservationId} not found");

            return reservation;
        }
    }
}