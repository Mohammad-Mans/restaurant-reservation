using RestaurantReservation.Db;
using RestaurantReservation.Db.Repositories;
using RestaurantReservation.Services;

Console.WriteLine("Restaurant Reservation System - CRUD Demonstration\n");

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

var restaurant = restaurantService.CreateRestaurant("Demo Restaurant", "123 Main St", "555-0001", "9AM-10PM");
var customer = customerService.CreateCustomer("John", "Doe", "john@email.com", "555-0002");
var employee = employeeService.CreateEmployee(restaurant.RestaurantId, "Jane", "Smith", "Manager");
var table = tableService.CreateTable(restaurant.RestaurantId, 4);
var menuItem = menuItemService.CreateMenuItem(restaurant.RestaurantId, "Burger", 12.99m, "Delicious burger");
var reservation = reservationService.CreateReservation(customer.CustomerId, restaurant.RestaurantId, table.TableId, DateTime.Now.AddDays(7), 4);
var order = orderService.CreateOrder(reservation.ReservationId, employee.EmployeeId, DateTime.Now, 45.98m);
var orderItem = orderItemService.CreateOrderItem(order.OrderId, menuItem.ItemId, 2);
Console.WriteLine("------ All entities created successfully!");

restaurantService.UpdateRestaurant(restaurant.RestaurantId, name: "Updated Restaurant");
customerService.UpdateCustomer(customer.CustomerId, email: "john.updated@email.com");
employeeService.UpdateEmployee(employee.EmployeeId, position: "Senior Manager");
tableService.UpdateTable(table.TableId, capacity: 6);
menuItemService.UpdateMenuItem(menuItem.ItemId, price: 14.99m);
reservationService.UpdateReservation(reservation.ReservationId, partySize: 6);
orderService.UpdateOrder(order.OrderId, totalAmount: 49.98m);
orderItemService.UpdateOrderItem(orderItem.OrderItemId, quantity: 3);
Console.WriteLine("------ All entities updated successfully!");

orderItemService.DeleteOrderItem(orderItem.OrderItemId);
orderService.DeleteOrder(order.OrderId);
reservationService.DeleteReservation(reservation.ReservationId);
menuItemService.DeleteMenuItem(menuItem.ItemId);
tableService.DeleteTable(table.TableId);
employeeService.DeleteEmployee(employee.EmployeeId);
customerService.DeleteCustomer(customer.CustomerId);
restaurantService.DeleteRestaurant(restaurant.RestaurantId);
Console.WriteLine("------ All entities deleted successfully!");

