using RestaurantReservation.Db;
using RestaurantReservation.Db.Repositories;
using RestaurantReservation.Services;
using RestaurantReservation.UI;

Console.WriteLine("=== Restaurant Reservation System ===\n");

using var context = new RestaurantReservationDbContext();

var restaurantRepository = new RestaurantRepository(context);
var customerRepository = new CustomerRepository(context);
var employeeRepository = new EmployeeRepository(context);
var tableRepository = new TableRepository(context);
var menuItemRepository = new MenuItemRepository(context);
var reservationRepository = new ReservationRepository(context);
var orderRepository = new OrderRepository(context);
var orderItemRepository = new OrderItemRepository(context);

var restaurantService = new RestaurantService(restaurantRepository);
var customerService = new CustomerService(customerRepository);
var employeeService = new EmployeeService(employeeRepository);
var tableService = new TableService(tableRepository);
var menuItemService = new MenuItemService(menuItemRepository);
var reservationService = new ReservationService(reservationRepository);
var orderService = new OrderService(orderRepository);
var orderItemService = new OrderItemService(orderItemRepository);

var menu = new Menu(
    restaurantService,
    customerService,
    employeeService,
    tableService,
    menuItemService,
    reservationService,
    orderService,
    orderItemService);

menu.Show();