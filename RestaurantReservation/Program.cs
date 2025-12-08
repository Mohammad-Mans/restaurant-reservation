using Microsoft.EntityFrameworkCore;
using RestaurantReservation.Db;
using RestaurantReservation.Db.Repositories;
using RestaurantReservation.Services;
using RestaurantReservation.UI;

await MainAsync();

static async Task MainAsync()
{
    Console.WriteLine("=== Restaurant Reservation System ===\n");

    var optionsBuilder = new DbContextOptionsBuilder<RestaurantReservationDbContext>();
    optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=RestaurantReservationCore;Integrated Security=True;TrustServerCertificate=True");

    using var context = new RestaurantReservationDbContext(optionsBuilder.Options);

    var restaurantRepository = new RestaurantRepository(context);
    var customerRepository = new CustomerRepository(context);
    var employeeRepository = new EmployeeRepository(context);
    var tableRepository = new TableRepository(context);
    var menuItemRepository = new MenuItemRepository(context);
    var reservationRepository = new ReservationRepository(context);
    var orderRepository = new OrderRepository(context);
    var orderItemRepository = new OrderItemRepository(context);
    var reservationDetailsViewRepository = new ReservationDetailsViewRepository(context);
    var employeeDetailsViewRepository = new EmployeeDetailsViewRepository(context);

    var restaurantService = new RestaurantService(restaurantRepository);
    var customerService = new CustomerService(customerRepository);
    var employeeService = new EmployeeService(employeeRepository);
    var tableService = new TableService(tableRepository);
    var menuItemService = new MenuItemService(menuItemRepository);
    var reservationService = new ReservationService(reservationRepository);
    var orderService = new OrderService(orderRepository);
    var orderItemService = new OrderItemService(orderItemRepository);
    var reservationDetailsViewService = new ReservationDetailsViewService(reservationDetailsViewRepository);
    var employeeDetailsViewService = new EmployeeDetailsViewService(employeeDetailsViewRepository);

    var menu = new Menu(
        restaurantService,
        customerService,
        employeeService,
        tableService,
        menuItemService,
        reservationService,
        orderService,
        orderItemService,
        reservationDetailsViewService,
        employeeDetailsViewService);

    await menu.ShowAsync();
}