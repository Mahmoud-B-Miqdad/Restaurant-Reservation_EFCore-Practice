using AutoMapper;
using RestaurantReservation.Db.Entities;
using RestaurantReservation.Db.Repositories.Interfaces;
using RestaurantReservationSystem.API.DTOs.Requests;
using RestaurantReservationSystem.API.DTOs.Responses;
using RestaurantReservationSystem.API.Services.Interfaces;

namespace RestaurantReservationSystem.API.Services
{
    /// <summary>
    /// Provides business logic and service methods for managing reservation operations.
    /// </summary>
    public class ReservationService : IReservationService
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IMenuItemRepository _menuItemRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReservationService"/> class.
        /// </summary>
        /// <param name="repository">The repository responsible for reservation data access.</param>
        /// <param name="mapper">The mapper used to convert between entities and DTOs.</param>
        public ReservationService(IReservationRepository repository, IMapper mapper, IOrderRepository orderRepository, IMenuItemRepository menuItemRepository)
        {
            _reservationRepository = repository;
            _orderRepository = orderRepository;
            _mapper = mapper;
            _menuItemRepository = menuItemRepository;
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
            var reservation = await _reservationRepository.GetByIdAsync(id);
            return reservation == null ? null : _mapper.Map<ReservationResponse>(reservation);
        }

        /// <inheritdoc />
        public async Task<ReservationResponse> CreateAsync(ReservationRequest request)
        {
            var reservation = _mapper.Map<Reservation>(request);
            await _reservationRepository.AddAsync(reservation);
            return _mapper.Map<ReservationResponse>(reservation);
        }

        /// <inheritdoc />
        public async Task<ReservationResponse?> UpdateAsync(int id, ReservationRequest request)
        {
            var updatedReservation = await _reservationRepository.GetByIdAsync(id);
            if (updatedReservation == null) return null;

            _mapper.Map(request, updatedReservation);
            await _reservationRepository.UpdateAsync(updatedReservation);
            return _mapper.Map<ReservationResponse>(updatedReservation);
        }

        /// <inheritdoc />
        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _reservationRepository.GetByIdAsync(id);
            if (existing == null) return false;

            await _reservationRepository.DeleteAsync(id);
            return true;
        }

        /// <inheritdoc />
        public async Task<CustomerResponse?> GetCustomerAsync(int reservationId)
        {
            var customer = await _reservationRepository.GetCustomerByReservationIdAsync(reservationId);
            return customer == null ? null : _mapper.Map<CustomerResponse>(customer);
        }

        /// <inheritdoc />
        public async Task<RestaurantResponse?> GetRestaurantAsync(int reservationId)
        {
            var restaurant = await _reservationRepository.GetRestaurantByReservationIdAsync(reservationId);
            return restaurant == null ? null : _mapper.Map<RestaurantResponse>(restaurant);
        }

        /// <inheritdoc />
        public async Task<TableResponse?> GetTableAsync(int reservationId)
        {
            var table = await _reservationRepository.GetTableByReservationIdAsync(reservationId);
            return table == null ? null : _mapper.Map<TableResponse>(table);
        }

        /// <inheritdoc />
        public async Task<IEnumerable<OrderResponse>> GetOrdersAndMenuItemsAsync(int reservationId)
        {
            var orders = await _orderRepository.ListOrdersAndMenuItemsAsync(reservationId);
            return _mapper.Map<IEnumerable<OrderResponse>>(orders);
        }

        /// <inheritdoc />
        public async Task<IEnumerable<ReservationResponse>> GetReservationsByCustomerAsync(int customerId)
        {
            var reservationsByCustomer = await _reservationRepository.GetReservationsByCustomerAsync(customerId);
            return _mapper.Map<IEnumerable<ReservationResponse>>(reservationsByCustomer);
        }

        /// <inheritdoc />
        public async Task<IEnumerable<MenuItemResponse>> GetOrderedMenuItemsAsync(int reservationId)
        {
            var orderedMenuItems = await _menuItemRepository.ListOrderedMenuItemsAsync(reservationId);
            return _mapper.Map<IEnumerable<MenuItemResponse>>(orderedMenuItems);
        }
    }
}