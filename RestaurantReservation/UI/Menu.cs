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
    OrderItemService orderItemService,
    ReservationDetailsViewService reservationDetailsViewService,
    EmployeeDetailsViewService employeeDetailsViewService)
{
    public async Task ShowAsync()
    {
        var exit = false;

        while (!exit)
        {
            ShowMenu();
            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    await PerformCrudOperationsAsync();
                    break;
                case "2":
                    await ListAllManagersAsync();
                    break;
                case "3":
                    await GetReservationsByCustomerAsync();
                    break;
                case "4":
                    await ListOrdersAndMenuItemsAsync();
                    break;
                case "5":
                    await ListOrderedMenuItemsAsync();
                    break;
                case "6":
                    await CalculateAverageOrderAmountAsync();
                    break;
                case "7":
                    await UseViewsAsync();
                    break;
                case "8":
                    await CalculateRestaurantRevenueAsync();
                    break;
                case "9":
                    await FindCustomersByPartySizeAsync();
                    break;
                case "10":
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
        Console.WriteLine("  6. Calculate Average Order Amount     ");
        Console.WriteLine("  7. Use Views                           ");
        Console.WriteLine("  8. Calculate Restaurant Revenue          ");
        Console.WriteLine("  9. Find Customers by Party Size          ");
        Console.WriteLine(" 10. Exit                               ");
        Console.WriteLine("========================================");
        Console.Write("\nSelect an option: ");
    }

    private async Task PerformCrudOperationsAsync()
    {
        Console.WriteLine("\n=== Performing CRUD Operations on All Entities ===\n");

        // Create operations
        var restaurant = await restaurantService.CreateRestaurantAsync("Demo Restaurant", "123 Main St", "555-0001", "9AM-10PM");
        var customer = await customerService.CreateCustomerAsync("John", "Doe", "john@email.com", "555-0002");
        var employee = await employeeService.CreateEmployeeAsync(restaurant.RestaurantId, "Jane", "Smith", "Manager");
        var table = await tableService.CreateTableAsync(restaurant.RestaurantId, 4);
        var menuItem = await menuItemService.CreateMenuItemAsync(restaurant.RestaurantId, "Burger", 12.99m, "Delicious burger");
        var reservation = await reservationService.CreateReservationAsync(customer.CustomerId, restaurant.RestaurantId,
            table.TableId,
            DateTime.Now.AddDays(7), 4);
        var order = await orderService.CreateOrderAsync(reservation.ReservationId, employee.EmployeeId, DateTime.Now, 45.98m);
        var orderItem = await orderItemService.CreateOrderItemAsync(order.OrderId, menuItem.ItemId, 2);
        Console.WriteLine("------ All entities created successfully!");

        // Update operations
        await restaurantService.UpdateRestaurantAsync(restaurant.RestaurantId, name: "Updated Restaurant");
        await customerService.UpdateCustomerAsync(customer.CustomerId, email: "john.updated@email.com");
        await employeeService.UpdateEmployeeAsync(employee.EmployeeId, position: "Senior Manager");
        await tableService.UpdateTableAsync(table.TableId, capacity: 6);
        await menuItemService.UpdateMenuItemAsync(menuItem.ItemId, price: 14.99m);
        await reservationService.UpdateReservationAsync(reservation.ReservationId, partySize: 6);
        await orderService.UpdateOrderAsync(order.OrderId, totalAmount: 49.98m);
        await orderItemService.UpdateOrderItemAsync(orderItem.OrderItemId, quantity: 3);
        Console.WriteLine("------ All entities updated successfully!");

        // Delete operations (in reverse dependency order)
        await orderItemService.DeleteOrderItemAsync(orderItem.OrderItemId);
        await orderService.DeleteOrderAsync(order.OrderId);
        await reservationService.DeleteReservationAsync(reservation.ReservationId);
        await menuItemService.DeleteMenuItemAsync(menuItem.ItemId);
        await tableService.DeleteTableAsync(table.TableId);
        await employeeService.DeleteEmployeeAsync(employee.EmployeeId);
        await customerService.DeleteCustomerAsync(customer.CustomerId);
        await restaurantService.DeleteRestaurantAsync(restaurant.RestaurantId);
        Console.WriteLine("------ All entities deleted successfully!\n");

        Console.WriteLine("Press any key to return to main menu...");
        Console.ReadKey();
        Console.WriteLine();
    }

    private async Task ListAllManagersAsync()
    {
        Console.WriteLine("\n=== List of All Managers ===\n");

        var managers = await employeeService.ListManagersAsync();

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

    private async Task GetReservationsByCustomerAsync()
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

        var reservations = await reservationService.GetReservationsByCustomerAsync(customerId);

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

    private async Task ListOrdersAndMenuItemsAsync()
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

        var orders = await orderService.ListOrdersAndMenuItemsAsync(reservationId);

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

    private async Task ListOrderedMenuItemsAsync()
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

        var orderedMenuItems = await menuItemService.ListOrderedMenuItemsAsync(reservationId);

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

    private async Task CalculateAverageOrderAmountAsync()
    {
        Console.WriteLine("\n=== Calculate Average Order Amount ===\n");
        Console.Write("Enter Employee ID: ");

        if (!int.TryParse(Console.ReadLine(), out var employeeId))
        {
            Console.WriteLine("\nInvalid Employee ID. Please enter a valid number.\n");
            Console.WriteLine("Press any key to return to main menu...");
            Console.ReadKey();
            Console.WriteLine();
            return;
        }

        var averageAmount = await orderService.CalculateAverageOrderAmountAsync(employeeId);

        Console.WriteLine($"\n=== Average Order Amount for Employee ID {employeeId} ===\n");

        if (averageAmount == null)
        {
            Console.WriteLine("No orders found for this employee.");
        }
        else
        {
            Console.WriteLine($"Average Order Amount: ${averageAmount.Value:F2}");
        }

        Console.WriteLine("\nPress any key to return to main menu...");
        Console.ReadKey();
        Console.WriteLine();
    }

    private async Task CalculateRestaurantRevenueAsync()
    {
        Console.WriteLine("\n=== Calculate Restaurant Revenue ===\n");
        Console.Write("Enter Restaurant ID: ");

        if (!int.TryParse(Console.ReadLine(), out var restaurantId))
        {
            Console.WriteLine("\nInvalid Restaurant ID. Please enter a valid number.\n");
            Console.WriteLine("Press any key to return to main menu...");
            Console.ReadKey();
            Console.WriteLine();
            return;
        }

        var restaurant = await restaurantService.GetRestaurantByIdAsync(restaurantId);
        if (restaurant == null)
        {
            Console.WriteLine($"\nRestaurant with ID {restaurantId} not found.");
            Console.WriteLine("\nPress any key to return to main menu...");
            Console.ReadKey();
            Console.WriteLine();
            return;
        }

        var revenue = await restaurantService.CalculateRestaurantRevenueAsync(restaurantId);

        Console.WriteLine($"\n=== Revenue for {restaurant.Name} ===\n");
        Console.WriteLine($"Restaurant ID: {restaurantId}");
        Console.WriteLine($"Restaurant Name: {restaurant.Name}");
        Console.WriteLine($"Total Revenue: ${revenue:F2}");

        Console.WriteLine("\nPress any key to return to main menu...");
        Console.ReadKey();
        Console.WriteLine();
    }

    private async Task FindCustomersByPartySizeAsync()
    {
        Console.WriteLine("\n=== Find Customers by Party Size (Stored Procedure) ===\n");
        Console.Write("Enter minimum party size: ");

        if (!int.TryParse(Console.ReadLine(), out var minPartySize))
        {
            Console.WriteLine("\nInvalid party size. Please enter a valid number.\n");
            Console.WriteLine("Press any key to return to main menu...");
            Console.ReadKey();
            Console.WriteLine();
            return;
        }

        var customers = await customerService.FindCustomersByPartySizeAsync(minPartySize);

        Console.WriteLine($"\n=== Customers with Reservations Party Size > {minPartySize} ===\n");

        if (customers.Count == 0)
        {
            Console.WriteLine("No customers found with reservations exceeding the specified party size.");
        }
        else
        {
            Console.WriteLine($"Found {customers.Count} customer(s):\n");
            Console.WriteLine($"{"ID",-5} {"First Name",-15} {"Last Name",-15} {"Email",-30} {"Phone",-15}");
            Console.WriteLine(new string('-', 80));

            foreach (var customer in customers)
            {
                var email = customer.Email ?? "N/A";
                var phone = customer.PhoneNumber ?? "N/A";
                Console.WriteLine(
                    $"{customer.CustomerId,-5} {customer.FirstName,-15} {customer.LastName,-15} {email,-30} {phone,-15}");
            }
        }

        Console.WriteLine("\nPress any key to return to main menu...");
        Console.ReadKey();
        Console.WriteLine();
    }

    private async Task UseViewsAsync()
    {
        var exit = false;

        while (!exit)
        {
            Console.WriteLine("\n=== Use Views ===");
            Console.WriteLine("  1. View All Reservation Details         ");
            Console.WriteLine("  2. View Reservation Details by ID       ");
            Console.WriteLine("  3. View Reservations by Customer ID     ");
            Console.WriteLine("  4. View Reservations by Restaurant ID  ");
            Console.WriteLine("  5. View All Employee Details           ");
            Console.WriteLine("  6. View Employee Details by ID          ");
            Console.WriteLine("  7. View Employees by Restaurant ID      ");
            Console.WriteLine("  8. Back to Main Menu                    ");
            Console.Write("\nSelect an option: ");

            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    await ViewAllReservationDetailsAsync();
                    break;
                case "2":
                    await ViewReservationDetailsByIdAsync();
                    break;
                case "3":
                    await ViewReservationsByCustomerIdAsync();
                    break;
                case "4":
                    await ViewReservationsByRestaurantIdAsync();
                    break;
                case "5":
                    await ViewAllEmployeeDetailsAsync();
                    break;
                case "6":
                    await ViewEmployeeDetailsByIdAsync();
                    break;
                case "7":
                    await ViewEmployeesByRestaurantIdAsync();
                    break;
                case "8":
                    exit = true;
                    break;
                default:
                    Console.WriteLine("\nInvalid option. Please try again.");
                    break;
            }
        }
    }

    private async Task ViewAllReservationDetailsAsync()
    {
        Console.WriteLine("\n=== All Reservation Details (from View) ===\n");

        var reservations = await reservationDetailsViewService.GetAllAsync();

        if (reservations.Count == 0)
        {
            Console.WriteLine("No reservations found.");
        }
        else
        {
            Console.WriteLine($"Found {reservations.Count} reservation(s):\n");
            Console.WriteLine(
                $"{"Reservation ID",-15} {"Date",-20} {"Party Size",-12} {"Customer Name",-25} {"Restaurant Name",-30}");
            Console.WriteLine(new string('-', 102));

            foreach (var reservation in reservations)
            {
                var formattedDate = reservation.ReservationDate.ToString("yyyy-MM-dd HH:mm");
                var customerName = $"{reservation.CustomerFirstName} {reservation.CustomerLastName}";
                Console.WriteLine(
                    $"{reservation.ReservationId,-15} {formattedDate,-20} {reservation.PartySize,-12} {customerName,-25} {reservation.RestaurantName,-30}");
            }
        }

        Console.WriteLine("\nPress any key to continue...");
        Console.ReadKey();
    }

    private async Task ViewReservationDetailsByIdAsync()
    {
        Console.WriteLine("\n=== View Reservation Details by ID ===\n");
        Console.Write("Enter Reservation ID: ");

        if (!int.TryParse(Console.ReadLine(), out var reservationId))
        {
            Console.WriteLine("\nInvalid Reservation ID. Please enter a valid number.\n");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            return;
        }

        var reservation = await reservationDetailsViewService.GetByReservationIdAsync(reservationId);

        Console.WriteLine($"\n=== Reservation Details for ID {reservationId} ===\n");

        if (reservation == null)
        {
            Console.WriteLine("Reservation not found.");
        }
        else
        {
            var formattedDate = reservation.ReservationDate.ToString("yyyy-MM-dd HH:mm:ss");
            Console.WriteLine($"Reservation ID: {reservation.ReservationId}");
            Console.WriteLine($"Reservation Date: {formattedDate}");
            Console.WriteLine($"Party Size: {reservation.PartySize}");
            Console.WriteLine("\nCustomer Information:");
            Console.WriteLine($"  Customer ID: {reservation.CustomerId}");
            Console.WriteLine($"  Name: {reservation.CustomerFirstName} {reservation.CustomerLastName}");
            Console.WriteLine($"  Email: {reservation.CustomerEmail ?? "N/A"}");
            Console.WriteLine($"  Phone: {reservation.CustomerPhoneNumber ?? "N/A"}");
            Console.WriteLine("\nRestaurant Information:");
            Console.WriteLine($"  Restaurant ID: {reservation.RestaurantId}");
            Console.WriteLine($"  Name: {reservation.RestaurantName}");
            Console.WriteLine($"  Address: {reservation.RestaurantAddress ?? "N/A"}");
            Console.WriteLine($"  Phone: {reservation.RestaurantPhoneNumber ?? "N/A"}");
            Console.WriteLine($"  Opening Hours: {reservation.RestaurantOpeningHours ?? "N/A"}");
        }

        Console.WriteLine("\nPress any key to continue...");
        Console.ReadKey();
    }

    private async Task ViewReservationsByCustomerIdAsync()
    {
        Console.WriteLine("\n=== View Reservations by Customer ID ===\n");
        Console.Write("Enter Customer ID: ");

        if (!int.TryParse(Console.ReadLine(), out var customerId))
        {
            Console.WriteLine("\nInvalid Customer ID. Please enter a valid number.\n");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            return;
        }

        var reservations = await reservationDetailsViewService.GetByCustomerIdAsync(customerId);

        Console.WriteLine($"\n=== Reservations for Customer ID {customerId} ===\n");

        if (reservations.Count == 0)
        {
            Console.WriteLine("No reservations found for this customer.");
        }
        else
        {
            Console.WriteLine($"Found {reservations.Count} reservation(s):\n");
            Console.WriteLine(
                $"{"Reservation ID",-15} {"Date",-20} {"Party Size",-12} {"Restaurant Name",-30}");
            Console.WriteLine(new string('-', 77));

            foreach (var reservation in reservations)
            {
                var formattedDate = reservation.ReservationDate.ToString("yyyy-MM-dd HH:mm");
                Console.WriteLine(
                    $"{reservation.ReservationId,-15} {formattedDate,-20} {reservation.PartySize,-12} {reservation.RestaurantName,-30}");
            }
        }

        Console.WriteLine("\nPress any key to continue...");
        Console.ReadKey();
    }

    private async Task ViewReservationsByRestaurantIdAsync()
    {
        Console.WriteLine("\n=== View Reservations by Restaurant ID ===\n");
        Console.Write("Enter Restaurant ID: ");

        if (!int.TryParse(Console.ReadLine(), out var restaurantId))
        {
            Console.WriteLine("\nInvalid Restaurant ID. Please enter a valid number.\n");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            return;
        }

        var reservations = await reservationDetailsViewService.GetByRestaurantIdAsync(restaurantId);

        Console.WriteLine($"\n=== Reservations for Restaurant ID {restaurantId} ===\n");

        if (reservations.Count == 0)
        {
            Console.WriteLine("No reservations found for this restaurant.");
        }
        else
        {
            Console.WriteLine($"Found {reservations.Count} reservation(s):\n");
            Console.WriteLine(
                $"{"Reservation ID",-15} {"Date",-20} {"Party Size",-12} {"Customer Name",-25}");
            Console.WriteLine(new string('-', 72));

            foreach (var reservation in reservations)
            {
                var formattedDate = reservation.ReservationDate.ToString("yyyy-MM-dd HH:mm");
                var customerName = $"{reservation.CustomerFirstName} {reservation.CustomerLastName}";
                Console.WriteLine(
                    $"{reservation.ReservationId,-15} {formattedDate,-20} {reservation.PartySize,-12} {customerName,-25}");
            }
        }

        Console.WriteLine("\nPress any key to continue...");
        Console.ReadKey();
    }

    private async Task ViewAllEmployeeDetailsAsync()
    {
        Console.WriteLine("\n=== All Employee Details (from View) ===\n");

        var employees = await employeeDetailsViewService.GetAllAsync();

        if (employees.Count == 0)
        {
            Console.WriteLine("No employees found.");
        }
        else
        {
            Console.WriteLine($"Found {employees.Count} employee(s):\n");
            Console.WriteLine(
                $"{"Employee ID",-12} {"Name",-25} {"Position",-20} {"Restaurant Name",-30}");
            Console.WriteLine(new string('-', 87));

            foreach (var employee in employees)
            {
                var employeeName = $"{employee.FirstName} {employee.LastName}";
                Console.WriteLine(
                    $"{employee.EmployeeId,-12} {employeeName,-25} {employee.Position,-20} {employee.RestaurantName,-30}");
            }
        }

        Console.WriteLine("\nPress any key to continue...");
        Console.ReadKey();
    }

    private async Task ViewEmployeeDetailsByIdAsync()
    {
        Console.WriteLine("\n=== View Employee Details by ID ===\n");
        Console.Write("Enter Employee ID: ");

        if (!int.TryParse(Console.ReadLine(), out var employeeId))
        {
            Console.WriteLine("\nInvalid Employee ID. Please enter a valid number.\n");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            return;
        }

        var employee = await employeeDetailsViewService.GetByEmployeeIdAsync(employeeId);

        Console.WriteLine($"\n=== Employee Details for ID {employeeId} ===\n");

        if (employee == null)
        {
            Console.WriteLine("Employee not found.");
        }
        else
        {
            Console.WriteLine($"Employee ID: {employee.EmployeeId}");
            Console.WriteLine($"Name: {employee.FirstName} {employee.LastName}");
            Console.WriteLine($"Position: {employee.Position}");
            Console.WriteLine("\nRestaurant Information:");
            Console.WriteLine($"  Restaurant ID: {employee.RestaurantId}");
            Console.WriteLine($"  Name: {employee.RestaurantName}");
            Console.WriteLine($"  Address: {employee.RestaurantAddress ?? "N/A"}");
            Console.WriteLine($"  Phone: {employee.RestaurantPhoneNumber ?? "N/A"}");
            Console.WriteLine($"  Opening Hours: {employee.RestaurantOpeningHours ?? "N/A"}");
        }

        Console.WriteLine("\nPress any key to continue...");
        Console.ReadKey();
    }

    private async Task ViewEmployeesByRestaurantIdAsync()
    {
        Console.WriteLine("\n=== View Employees by Restaurant ID ===\n");
        Console.Write("Enter Restaurant ID: ");

        if (!int.TryParse(Console.ReadLine(), out var restaurantId))
        {
            Console.WriteLine("\nInvalid Restaurant ID. Please enter a valid number.\n");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            return;
        }

        var employees = await employeeDetailsViewService.GetByRestaurantIdAsync(restaurantId);

        Console.WriteLine($"\n=== Employees for Restaurant ID {restaurantId} ===\n");

        if (employees.Count == 0)
        {
            Console.WriteLine("No employees found for this restaurant.");
        }
        else
        {
            Console.WriteLine($"Found {employees.Count} employee(s):\n");
            Console.WriteLine(
                $"{"Employee ID",-12} {"Name",-25} {"Position",-20}");
            Console.WriteLine(new string('-', 57));

            foreach (var employee in employees)
            {
                var employeeName = $"{employee.FirstName} {employee.LastName}";
                Console.WriteLine(
                    $"{employee.EmployeeId,-12} {employeeName,-25} {employee.Position,-20}");
            }
        }

        Console.WriteLine("\nPress any key to continue...");
        Console.ReadKey();
    }
}