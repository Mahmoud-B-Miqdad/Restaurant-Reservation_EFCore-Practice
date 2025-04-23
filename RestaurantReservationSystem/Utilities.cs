using RestaurantReservation.Db.Repositories;
using RestaurantReservation.Db.Repositories.ReportRepositories;
using RestaurantReservation.Db.Seeders;

public class AppUtilities
{
    private readonly RestaurantReservationSeeder _DbSeeder;
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
    private readonly MenuItemRepository _menuItemRepository;
    private readonly ReservationReportRepository _reservationReportRepo;
    private readonly EmployeeReportRepository _employeeReportRepository;
    private readonly RevenueReportRepository _revenueReportRepository;
    private readonly CustomerReportRepository _customerReportRepository;

    public AppUtilities(RestaurantReservationSeeder DbSeeder, RestaurantService restaurantService,
        CustomerService customerService, EmployeeService employeeService,
        ReservationService reservationService, TableService tableServices,
        MenuItemService menuItemServices, OrderService orderServices, OrderItemService orderItemServices
        , EmployeeRepository employeeRepository,ReservationRepository reservationRepository
        , OrderRepository orderRepository, MenuItemRepository menuItemRepository, 
        ReservationReportRepository reservationReportRepo, EmployeeReportRepository employeeReportRepository
        , RevenueReportRepository revenueReportRepository, CustomerReportRepository customerReportRepository)
    {
        _DbSeeder = DbSeeder;
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
        _menuItemRepository = menuItemRepository;
        _reservationReportRepo = reservationReportRepo;
        _employeeReportRepository = employeeReportRepository;
        _revenueReportRepository = revenueReportRepository;
        _customerReportRepository = customerReportRepository;
    }

    public async Task RunAsync()
    {
        //await _DbSeeder.SeedAsync();

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

        //int reservationId = 1;

        //var ordersAndMenuItems = await _orderRepository.ListOrdersAndMenuItemsAsync(reservationId);

        //foreach (var order in ordersAndMenuItems)
        //{
        //    Console.WriteLine($"Order ID: {order.OrderId}, Reservation ID: {order.ReservationId}" +
        //        $" Order Date {order.OrderDate} Order Amount {order.TotalAmount}");

        //    foreach (var orderItem in order.OrderItems)
        //    {
        //        Console.WriteLine($"Menu Item: {orderItem.MenuItem.Name}, Quantity: {orderItem.Quantity}");
        //    }
        //}

        //int reservationId = 1;

        //var orderedMenuItems = await _menuItemRepository.ListOrderedMenuItemsAsync(reservationId);

        //foreach (var menuItem in orderedMenuItems)
        //{
        //    Console.WriteLine($"Menu Item: {menuItem.Name}, Price: {menuItem.Price}");
        //}

        //int employeeId = 1; 

        //var averageOrderAmount = await _orderRepository.CalculateAverageOrderAmountAsync(employeeId);

        //Console.WriteLine($"Average Order Amount for Employee {employeeId}: {averageOrderAmount:C}");

        //var reservations = await _reservationReportRepo.GetReservationsAsync();

        //foreach (var r in reservations)
        //{
        //    Console.WriteLine($"Customer: {r.CustomerFirstName} {r.CustomerLastName}, Restaurant: {r.RestaurantName}, Date: {r.ReservationDate}");
        //}

        //var employee = await _employeeReportRepository.GetEmployeesAsync();

        //foreach (var e in employee)
        //{
        //    Console.WriteLine($"Employee: {e.Employee_First_Name} {e.Employee_Last_Name}, Position: {e.Position}" +
        //        $" Restaurant: {e.Restaurant_Name}");
        //}

        //var revenue = await _revenueReportRepository.GetTotalRevenueByRestaurantAsync(1);
        //Console.WriteLine($"Total Revenue for Restaurant #1: {revenue} $");


        //var customers = await _customerReportRepository.GetCustomersByPartySizeAsync(4);
        //foreach (var c in customers)
        //{
        //    Console.WriteLine($"Customer: {c.FirstName} {c.LastName} - Email: {c.Email} - Phone: {c.PhoneNumber}" +
        //        $"PartySize {c.PartySize}");
        //}
    }
}
