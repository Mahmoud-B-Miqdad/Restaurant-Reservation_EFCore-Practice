using RestaurantReservationSystem.Domain.Exceptions;
using RestaurantReservationSystem.Domain.Interfaces.Repositories;
using RestaurantReservationSystem.Domain.Models;

namespace RestaurantReservationSystem.Domain.Validators
{
    public class MenuItemValidator
    {
        private readonly IMenuItemRepository _menuItemRepository;
        public MenuItemValidator(IMenuItemRepository menuItemRepository)
        {
            _menuItemRepository = menuItemRepository;
        }

        public async Task<MenuItemModel> EnsureMenuItemExistsAsync(int menuItemId)
        {
            var menuItem = await _menuItemRepository.GetByIdAsync(menuItemId);
            if (menuItem == null)
                throw new NotFoundException($"MenuItem with ID {menuItemId} not found");

            return menuItem;
        }
    }
}