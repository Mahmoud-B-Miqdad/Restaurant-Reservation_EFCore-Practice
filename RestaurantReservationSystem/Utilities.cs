using RestaurantReservation.Db;
using RestaurantReservation.Db.Models;
using RestaurantReservation.Db.Repositories;
using RestaurantReservation.Db.Seeders;

public class AppUtilities
{
    private readonly RestaurantReservationDbContext _context;
    private readonly RestaurantService _restaurantService;
    private readonly CustomerService _customerService;
    private readonly EmployeeService _employeeService;
    private readonly ReservationService _reservationServices;
    private readonly TableService _tableServices;
    private readonly MenuItemService _menuItemServices;
    private readonly OrderService _orderServices;
    private readonly OrderItemService _orderItemServices;
    private readonly EmployeeRepository _employeeRepository;
    private readonly ReservationRepository _reservationRepository;
    private readonly OrderRepository _orderRepository;

    public AppUtilities(RestaurantReservationDbContext context, RestaurantService restaurantService,
        CustomerService customerService, EmployeeService employeeService,
        ReservationService reservationService, TableService tableServices,
        MenuItemService menuItemServices, OrderService orderServices, OrderItemService orderItemServices
        , EmployeeRepository employeeRepository, ReservationRepository reservationRepository
        , OrderRepository orderRepository)
    {
        _context = context;
        _restaurantService = restaurantService;
        _customerService = customerService;
        _employeeService = employeeService;
        _reservationServices = reservationService;
        _tableServices = tableServices;
        _menuItemServices = menuItemServices;
        _orderServices = orderServices;
        _orderItemServices = orderItemServices;
        _employeeRepository = employeeRepository;
        _reservationRepository = reservationRepository;
        _orderRepository = orderRepository;
    }

    public async Task RunAsync()
    {
        //await RestaurantReservationSeeder.SeedAsync(_context);

        //try
        //{
        //    await _restaurantService.ExecuteExamplesAsync();
        //}
        //catch (InvalidOperationException ex)
        //{
        //    Console.WriteLine($"Operation failed: {ex.Message}");
        //}
        //catch (Exception ex)
        //{
        //    Console.WriteLine($"Unexpected error: {ex.Message}");
        //}

        //try
        //{
        //    await _customerService.ExecuteExamplesAsync();
        //}
        //catch (InvalidOperationException ex)
        //{
        //    Console.WriteLine($"Operation failed: {ex.Message}");
        //}
        //catch (Exception ex)
        //{
        //    Console.WriteLine($"Unexpected error: {ex.Message}");
        //}


        //try
        //{
        //    await _employeeService.ExecuteExamplesAsync();
        //}
        //catch (InvalidOperationException ex)
        //{
        //    Console.WriteLine($"Operation failed: {ex.Message}");
        //}
        //catch (Exception ex)
        //{
        //    Console.WriteLine($"Unexpected error: {ex.Message}");
        //}

        //try
        //{
        //    await _reservationServices.ExecuteExamplesAsync();
        //}
        //catch (InvalidOperationException ex)
        //{
        //    Console.WriteLine($"Operation failed: {ex.Message}");
        //}
        //catch (Exception ex)
        //{
        //    Console.WriteLine($"Unexpected error: {ex.Message}");
        //}

        //try
        //{
        //    await _tableServices.ExecuteExamplesAsync();
        //}
        //catch (InvalidOperationException ex)
        //{
        //    Console.WriteLine($"Operation failed: {ex.Message}");
        //}
        //catch (Exception ex)
        //{
        //    Console.WriteLine($"Unexpected error: {ex.Message}");
        //}

        //try
        //{
        //    await _menuItemServices.ExecuteExamplesAsync();
        //}
        //catch (InvalidOperationException ex)
        //{
        //    Console.WriteLine($"Operation failed: {ex.Message}");
        //}
        //catch (Exception ex)
        //{
        //    Console.WriteLine($"Unexpected error: {ex.Message}");
        //}

        //try
        //{
        //    await _orderServices.ExecuteExamplesAsync();
        //}
        //catch (InvalidOperationException ex)
        //{
        //    Console.WriteLine($"Operation failed: {ex.Message}");
        //}
        //catch (Exception ex)
        //{
        //    Console.WriteLine($"Unexpected error: {ex.Message}");
        //}

        //try
        //{
        //    await _orderItemServices.ExecuteExamplesAsync();
        //}
        //catch (InvalidOperationException ex)
        //{
        //    Console.WriteLine($"Operation failed: {ex.Message}");
        //}
        //catch (Exception ex)
        //{
        //    Console.WriteLine($"Unexpected error: {ex.Message}");
        //}

        //var managers = await _employeeRepository.ListManagersAsync();

        //foreach (var manager in managers)
        //{
        //    Console.WriteLine($"Manager: {manager.FirstName} {manager.LastName}");
        //}

        //var reservations = await _reservationRepository.GetReservationsByCustomerAsync(4);

        //foreach (var reservation in reservations)
        //{
        //    Console.WriteLine($"reservation Date: {reservation.ReservationDate} " +
        //        $"reservation PartySize {reservation.PartySize}");
        //}

        int reservationId = 2;  // قم بتغيير هذه القيمة حسب الحاجة

        var ordersAndMenuItems = await _orderRepository.ListOrdersAndMenuItemsAsync(reservationId);

        foreach (var order in ordersAndMenuItems)
        {
            Console.WriteLine($"Order ID: {order.OrderId}, Reservation ID: {order.ReservationId}" +
                $" Order Date {order.OrderDate} Order Amount {order.TotalAmount}");

            foreach (var orderItem in order.OrderItems)
            {
                Console.WriteLine($"Menu Item: {orderItem.MenuItem.Name}, Quantity: {orderItem.Quantity}");
            }
        }
    }
}
