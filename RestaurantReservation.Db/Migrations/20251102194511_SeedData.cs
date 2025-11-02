using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RestaurantReservation.Db.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "CustomerId", "Email", "FirstName", "LastName", "PhoneNumber" },
                values: new object[,]
                {
                    { 1, "john.smith@email.com", "John", "Smith", "555-1001" },
                    { 2, "emily.j@email.com", "Emily", "Johnson", "555-1002" },
                    { 3, "michael.w@email.com", "Michael", "Williams", "555-1003" },
                    { 4, "sarah.brown@email.com", "Sarah", "Brown", "555-1004" },
                    { 5, "david.d@email.com", "David", "Davis", "555-1005" }
                });

            migrationBuilder.InsertData(
                table: "Restaurants",
                columns: new[] { "RestaurantId", "Address", "Name", "OpeningHours", "PhoneNumber" },
                values: new object[,]
                {
                    { 1, "123 Main Street, Downtown", "Bella Vista Italian", "11:00 AM - 10:00 PM", "555-0101" },
                    { 2, "456 Harbor Drive, Waterfront", "Ocean Breeze Seafood", "12:00 PM - 11:00 PM", "555-0102" },
                    { 3, "789 Oak Avenue, Uptown", "Grill Masters Steakhouse", "5:00 PM - 12:00 AM", "555-0103" },
                    { 4, "321 Pine Street, Chinatown", "Golden Dragon Asian Fusion", "11:30 AM - 10:30 PM", "555-0104" },
                    { 5, "654 Elm Boulevard, French Quarter", "Le Café Parisien", "8:00 AM - 9:00 PM", "555-0105" }
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "EmployeeId", "FirstName", "LastName", "Position", "RestaurantId" },
                values: new object[,]
                {
                    { 1, "Alice", "Martinez", "Head Chef", 1 },
                    { 2, "Bob", "Thompson", "Waiter", 1 },
                    { 3, "Carol", "Anderson", "Manager", 2 },
                    { 4, "Daniel", "Wilson", "Sous Chef", 2 },
                    { 5, "Emma", "Taylor", "Hostess", 3 }
                });

            migrationBuilder.InsertData(
                table: "MenuItems",
                columns: new[] { "ItemId", "Description", "Name", "Price", "RestaurantId" },
                values: new object[,]
                {
                    { 1, "Creamy pasta with bacon and parmesan", "Spaghetti Carbonara", 18.99m, 1 },
                    { 2, "Classic pizza with tomato, mozzarella, and basil", "Margherita Pizza", 16.50m, 1 },
                    { 3, "Fresh Atlantic salmon with lemon butter sauce", "Grilled Salmon", 24.99m, 2 },
                    { 4, "Fresh lobster meat on toasted brioche bun", "Lobster Roll", 29.99m, 2 },
                    { 5, "12oz prime ribeye with garlic mashed potatoes", "Ribeye Steak", 35.99m, 3 }
                });

            migrationBuilder.InsertData(
                table: "Tables",
                columns: new[] { "TableId", "Capacity", "RestaurantId" },
                values: new object[,]
                {
                    { 1, 2, 1 },
                    { 2, 4, 1 },
                    { 3, 6, 2 },
                    { 4, 2, 2 },
                    { 5, 8, 3 }
                });

            migrationBuilder.InsertData(
                table: "Reservations",
                columns: new[] { "ReservationId", "CustomerId", "PartySize", "ReservationDate", "RestaurantId", "TableId" },
                values: new object[,]
                {
                    { 1, 1, 2, new DateTime(2025, 11, 15, 19, 0, 0, 0, DateTimeKind.Unspecified), 1, 1 },
                    { 2, 2, 4, new DateTime(2025, 11, 16, 20, 0, 0, 0, DateTimeKind.Unspecified), 1, 2 },
                    { 3, 3, 6, new DateTime(2025, 11, 17, 18, 30, 0, 0, DateTimeKind.Unspecified), 2, 3 },
                    { 4, 4, 2, new DateTime(2025, 11, 18, 19, 30, 0, 0, DateTimeKind.Unspecified), 2, 4 },
                    { 5, 5, 8, new DateTime(2025, 11, 19, 20, 0, 0, 0, DateTimeKind.Unspecified), 3, 5 }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "OrderId", "EmployeeId", "OrderDate", "ReservationId", "TotalAmount" },
                values: new object[,]
                {
                    { 1, 2, new DateTime(2025, 11, 15, 19, 15, 0, 0, DateTimeKind.Unspecified), 1, 35.49m },
                    { 2, 2, new DateTime(2025, 11, 16, 20, 10, 0, 0, DateTimeKind.Unspecified), 2, 82.50m },
                    { 3, 3, new DateTime(2025, 11, 17, 18, 45, 0, 0, DateTimeKind.Unspecified), 3, 149.94m },
                    { 4, 3, new DateTime(2025, 11, 18, 19, 45, 0, 0, DateTimeKind.Unspecified), 4, 29.99m },
                    { 5, 5, new DateTime(2025, 11, 19, 20, 15, 0, 0, DateTimeKind.Unspecified), 5, 287.92m }
                });

            migrationBuilder.InsertData(
                table: "OrderItems",
                columns: new[] { "OrderItemId", "ItemId", "OrderId", "Quantity" },
                values: new object[,]
                {
                    { 1, 1, 1, 1 },
                    { 2, 2, 1, 1 },
                    { 3, 2, 2, 5 },
                    { 4, 3, 3, 6 },
                    { 5, 4, 4, 1 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "ItemId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "OrderItemId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "OrderItemId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "OrderItemId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "OrderItemId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "OrderItemId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Restaurants",
                keyColumn: "RestaurantId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Restaurants",
                keyColumn: "RestaurantId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "ItemId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "ItemId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "ItemId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "ItemId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "ReservationId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "ReservationId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "ReservationId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "ReservationId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "ReservationId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "TableId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Restaurants",
                keyColumn: "RestaurantId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "TableId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "TableId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "TableId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "TableId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Restaurants",
                keyColumn: "RestaurantId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Restaurants",
                keyColumn: "RestaurantId",
                keyValue: 2);
        }
    }
}
