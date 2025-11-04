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
                    GetReservationsByCustomer();
                    break;
                case "4":
                    ListOrdersAndMenuItems();
                    break;
                case "5":
                    ListOrderedMenuItems();
                    break;
                case "6":
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
        Console.WriteLine("  3. Get Reservations by Customer      ");
        Console.WriteLine("  4. List Orders and Menu Items         ");
        Console.WriteLine("  5. List Ordered Menu Items            ");
        Console.WriteLine("  6. Exit                               ");
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

    private void GetReservationsByCustomer()
    {
        Console.WriteLine("\n=== Get Reservations by Customer ===\n");
        Console.Write("Enter Customer ID: ");

        if (!int.TryParse(Console.ReadLine(), out var customerId))
        {
            Console.WriteLine("\nInvalid Customer ID. Please enter a valid number.\n");
            Console.WriteLine("Press any key to return to main menu...");
            Console.ReadKey();
            Console.WriteLine();
            return;
        }

        var reservations = reservationService.GetReservationsByCustomer(customerId);

        Console.WriteLine($"\n=== Reservations for Customer ID {customerId} ===\n");

        if (reservations.Count == 0)
        {
            Console.WriteLine("No reservations found for this customer.");
        }
        else
        {
            Console.WriteLine($"Found {reservations.Count} reservation(s):\n");
            Console.WriteLine(
                $"{"Reservation ID",-15} {"Restaurant ID",-15} {"Table ID",-12} {"Reservation Date",-20} {"Party Size",-12}");
            Console.WriteLine(new string('-', 75));

            foreach (var reservation in reservations)
            {
                var formattedDate = reservation.ReservationDate.ToString("yyyy-MM-dd HH:mm:ss");
                Console.WriteLine(
                    $"{reservation.ReservationId,-15} {reservation.RestaurantId,-15} {reservation.TableId,-12} {formattedDate,-20} {reservation.PartySize,-12}");
            }
        }

        Console.WriteLine("\nPress any key to return to main menu...");
        Console.ReadKey();
        Console.WriteLine();
    }

    private void ListOrdersAndMenuItems()
    {
        Console.WriteLine("\n=== List Orders and Menu Items ===\n");
        Console.Write("Enter Reservation ID: ");

        if (!int.TryParse(Console.ReadLine(), out var reservationId))
        {
            Console.WriteLine("\nInvalid Reservation ID. Please enter a valid number.\n");
            Console.WriteLine("Press any key to return to main menu...");
            Console.ReadKey();
            Console.WriteLine();
            return;
        }

        var orders = orderService.ListOrdersAndMenuItems(reservationId);

        Console.WriteLine($"\n=== Orders and Menu Items for Reservation ID {reservationId} ===\n");

        if (orders.Count == 0)
        {
            Console.WriteLine("No orders found for this reservation.");
        }
        else
        {
            Console.WriteLine($"Found {orders.Count} order(s):\n");

            foreach (var order in orders)
            {
                var formattedDate = order.OrderDate.ToString("yyyy-MM-dd HH:mm:ss");
                Console.WriteLine($"Order ID: {order.OrderId}");
                Console.WriteLine($"  Order Date: {formattedDate}");
                Console.WriteLine($"  Employee ID: {order.EmployeeId}");
                Console.WriteLine($"  Total Amount: ${order.TotalAmount:F2}");
                Console.WriteLine("  Menu Items:");

                if (order.OrderItems.Count == 0)
                {
                    Console.WriteLine("    No menu items found for this order.");
                }
                else
                {
                    Console.WriteLine($"    {"Item Name",-30} {"Quantity",-10} {"Price",-10} {"Subtotal",-10}");
                    Console.WriteLine($"    {new string('-', 60)}");

                    foreach (var orderItem in order.OrderItems)
                    {
                        var item = orderItem.Item;
                        var subtotal = item.Price * orderItem.Quantity;
                        Console.WriteLine(
                            $"    {item.Name,-30} {orderItem.Quantity,-10} ${item.Price,-9:F2} ${subtotal,-9:F2}");
                    }
                }

                Console.WriteLine();
            }
        }

        Console.WriteLine("Press any key to return to main menu...");
        Console.ReadKey();
        Console.WriteLine();
    }

    private void ListOrderedMenuItems()
    {
        Console.WriteLine("\n=== List Ordered Menu Items ===\n");
        Console.Write("Enter Reservation ID: ");

        if (!int.TryParse(Console.ReadLine(), out var reservationId))
        {
            Console.WriteLine("\nInvalid Reservation ID. Please enter a valid number.\n");
            Console.WriteLine("Press any key to return to main menu...");
            Console.ReadKey();
            Console.WriteLine();
            return;
        }

        var orderedMenuItems = menuItemService.ListOrderedMenuItems(reservationId);

        Console.WriteLine($"\n=== Ordered Menu Items for Reservation ID {reservationId} ===\n");

        if (orderedMenuItems.Count == 0)
        {
            Console.WriteLine("No menu items found for this reservation.");
        }
        else
        {
            Console.WriteLine($"Found {orderedMenuItems.Count} menu item(s):\n");
            Console.WriteLine($"{"Item Name",-30} {"Description",-40} {"Price",-10}");
            Console.WriteLine(new string('-', 80));

            foreach (var item in orderedMenuItems)
            {
                var description = item.Description ?? "N/A";
                Console.WriteLine(
                    $"{item.Name,-30} {description,-40} ${item.Price,-9:F2}");
            }
        }

        Console.WriteLine("\nPress any key to return to main menu...");
        Console.ReadKey();
        Console.WriteLine();
    }
}