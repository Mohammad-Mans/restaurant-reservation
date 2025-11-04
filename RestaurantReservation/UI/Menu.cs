using RestaurantReservation.Services;

namespace RestaurantReservation.UI;

public class Menu(
    RestaurantService restaurantService,
    CustomerService customerService,
    EmployeeService employeeService,
    TableService tableService,
    MenuItemService menuItemService,
    ReservationService reservationService,
    OrderService orderService,
    OrderItemService orderItemService)
{
    public void Show()
    {
        var exit = false;

        while (!exit)
        {
            ShowMenu();
            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    PerformCrudOperations();
                    break;
                case "2":
                    ListAllManagers();
                    break;
                case "3":
                    exit = true;
                    Console.WriteLine("\nThank you for using Restaurant Reservation System. Goodbye!");
                    break;
                default:
                    Console.WriteLine("\nInvalid option. Please try again.\n");
                    break;
            }
        }
    }

    private void ShowMenu()
    {
        Console.WriteLine("========================================");
        Console.WriteLine("     RESTAURANT RESERVATION SYSTEM      ");
        Console.WriteLine("========================================");
        Console.WriteLine("  1. Perform CRUD Operations            ");
        Console.WriteLine("  2. List All Managers                  ");
        Console.WriteLine("  3. Exit                               ");
        Console.WriteLine("========================================");
        Console.Write("\nSelect an option: ");
    }

    private void PerformCrudOperations()
    {
        Console.WriteLine("\n=== Performing CRUD Operations on All Entities ===\n");

        // Create operations
        var restaurant = restaurantService.CreateRestaurant("Demo Restaurant", "123 Main St", "555-0001", "9AM-10PM");
        var customer = customerService.CreateCustomer("John", "Doe", "john@email.com", "555-0002");
        var employee = employeeService.CreateEmployee(restaurant.RestaurantId, "Jane", "Smith", "Manager");
        var table = tableService.CreateTable(restaurant.RestaurantId, 4);
        var menuItem = menuItemService.CreateMenuItem(restaurant.RestaurantId, "Burger", 12.99m, "Delicious burger");
        var reservation = reservationService.CreateReservation(customer.CustomerId, restaurant.RestaurantId,
            table.TableId,
            DateTime.Now.AddDays(7), 4);
        var order = orderService.CreateOrder(reservation.ReservationId, employee.EmployeeId, DateTime.Now, 45.98m);
        var orderItem = orderItemService.CreateOrderItem(order.OrderId, menuItem.ItemId, 2);
        Console.WriteLine("------ All entities created successfully!");

        // Update operations
        restaurantService.UpdateRestaurant(restaurant.RestaurantId, name: "Updated Restaurant");
        customerService.UpdateCustomer(customer.CustomerId, email: "john.updated@email.com");
        employeeService.UpdateEmployee(employee.EmployeeId, position: "Senior Manager");
        tableService.UpdateTable(table.TableId, capacity: 6);
        menuItemService.UpdateMenuItem(menuItem.ItemId, price: 14.99m);
        reservationService.UpdateReservation(reservation.ReservationId, partySize: 6);
        orderService.UpdateOrder(order.OrderId, totalAmount: 49.98m);
        orderItemService.UpdateOrderItem(orderItem.OrderItemId, quantity: 3);
        Console.WriteLine("------ All entities updated successfully!");

        // Delete operations (in reverse dependency order)
        orderItemService.DeleteOrderItem(orderItem.OrderItemId);
        orderService.DeleteOrder(order.OrderId);
        reservationService.DeleteReservation(reservation.ReservationId);
        menuItemService.DeleteMenuItem(menuItem.ItemId);
        tableService.DeleteTable(table.TableId);
        employeeService.DeleteEmployee(employee.EmployeeId);
        customerService.DeleteCustomer(customer.CustomerId);
        restaurantService.DeleteRestaurant(restaurant.RestaurantId);
        Console.WriteLine("------ All entities deleted successfully!\n");

        Console.WriteLine("Press any key to return to main menu...");
        Console.ReadKey();
        Console.WriteLine();
    }

    private void ListAllManagers()
    {
        Console.WriteLine("\n=== List of All Managers ===\n");

        var managers = employeeService.ListManagers();

        if (managers.Count == 0)
        {
            Console.WriteLine("No managers found.");
        }
        else
        {
            Console.WriteLine($"Found {managers.Count} manager(s):\n");
            Console.WriteLine($"{"ID",-5} {"First Name",-15} {"Last Name",-15} {"Position",-15} {"Restaurant ID",-15}");
            Console.WriteLine(new string('-', 65));

            foreach (var manager in managers)
            {
                Console.WriteLine(
                    $"{manager.EmployeeId,-5} {manager.FirstName,-15} {manager.LastName,-15} {manager.Position,-15} {manager.RestaurantId,-15}");
            }
        }

        Console.WriteLine("\nPress any key to return to main menu...");
        Console.ReadKey();
        Console.WriteLine();
    }
}