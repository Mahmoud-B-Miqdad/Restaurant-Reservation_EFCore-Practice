using AutoMapper;
using RestaurantReservation.Db.Entities;
using RestaurantReservation.Db.Repositories.Interfaces;
using RestaurantReservationSystem.API.DTOs;
using RestaurantReservationSystem.API.Interfaces;

namespace RestaurantReservationSystem.API.Services
{
    public class RestaurantService : IRestaurantService
    {
        private readonly IRestaurantRepository _repository;
        private readonly IMapper _mapper;

        public RestaurantService(IRestaurantRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<RestaurantResponse>> GetAllAsync()
        {
            var restaurants = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<RestaurantResponse>>(restaurants);
        }

        public async Task<RestaurantResponse> GetByIdAsync(int id)
        {
            var restaurant = await _repository.GetByIdAsync(id);
            return _mapper.Map<RestaurantResponse>(restaurant);
        }

        public async Task<RestaurantResponse> CreateAsync(RestaurantRequest request)
        {
            var restaurant = _mapper.Map<Restaurant>(request);
            await _repository.AddAsync(restaurant);
            return _mapper.Map<RestaurantResponse>(restaurant);
        }

        public async Task<bool> UpdateAsync(int id, RestaurantRequest request)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null) return false;

            var updated = _mapper.Map(request, existing);
            await _repository.UpdateAsync(updated);
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null) return false;

            await _repository.DeleteAsync(id);
            return true;
        }
    }
}
