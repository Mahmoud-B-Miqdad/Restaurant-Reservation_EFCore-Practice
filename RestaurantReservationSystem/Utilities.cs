using RestaurantReservation.Db.Repositories.Interfaces;
using RestaurantReservation.Db.Repositories.ReportRepositories;
using RestaurantReservation.Db.Seeders;
using RestaurantReservation.Db.Services.Interfaces;
using RestaurantReservationSystem.EntityServices.Services.Interfaces;

public class AppUtilities
{
    private readonly RestaurantReservationSeeder _DbSeeder;
    private readonly IRestaurantService _restaurantService;
    private readonly ICustomerService _customerService;
    private readonly IEmployeeService _employeeService;
    private readonly IReservationService _reservationServices;
    private readonly ITableService _tableServices;
    private readonly IMenuItemService _menuItemServices;
    private readonly IOrderService _orderServices;
    private readonly IOrderItemService _orderItemServices;
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IReservationRepository _reservationRepository;
    private readonly IOrderRepository _orderRepository;
    private readonly IMenuItemRepository _menuItemRepository;
    private readonly IReservationReportRepository _reservationReportRepo;
    private readonly IEmployeeReportRepository _employeeReportRepository;
    private readonly IRevenueReportRepository _revenueReportRepository;
    private readonly ICustomerReportRepository _customerReportRepository;

    public AppUtilities(RestaurantReservationSeeder DbSeeder, IRestaurantService restaurantService,
        ICustomerService customerService, IEmployeeService employeeService,
        IReservationService reservationService, ITableService tableServices,
        IMenuItemService menuItemServices, IOrderService orderServices, IOrderItemService orderItemServices
        , IEmployeeRepository employeeRepository, IReservationRepository reservationRepository
        , IOrderRepository orderRepository, IMenuItemRepository menuItemRepository,
        IReservationReportRepository reservationReportRepo, IEmployeeReportRepository employeeReportRepository
        , IRevenueReportRepository revenueReportRepository, ICustomerReportRepository customerReportRepository)
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


        var customers = await _customerReportRepository.GetCustomersByPartySizeAsync(4);
        foreach (var c in customers)
        {
            Console.WriteLine($"Customer: {c.FirstName} {c.LastName} - Email: {c.Email} - Phone: {c.PhoneNumber}" +
                $"PartySize {c.PartySize}");
        }
    }
}
