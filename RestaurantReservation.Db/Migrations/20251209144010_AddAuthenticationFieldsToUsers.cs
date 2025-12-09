using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestaurantReservation.Db.Migrations
{
    /// <inheritdoc />
    public partial class AddAuthenticationFieldsToUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PasswordHash",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PasswordHash",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 1,
                columns: new[] { "PasswordHash", "Username" },
                values: new object[] { "$2a$11$Hq44m.DFvMzXk06H0zEXueWhN9KpW5neimum8TaGVWM.wzj6RscWa", "johnsmith" });

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 2,
                columns: new[] { "PasswordHash", "Username" },
                values: new object[] { "$2a$11$Hq44m.DFvMzXk06H0zEXueWhN9KpW5neimum8TaGVWM.wzj6RscWa", "emilyj" });

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 3,
                columns: new[] { "PasswordHash", "Username" },
                values: new object[] { "$2a$11$Hq44m.DFvMzXk06H0zEXueWhN9KpW5neimum8TaGVWM.wzj6RscWa", "michaelw" });

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 4,
                columns: new[] { "PasswordHash", "Username" },
                values: new object[] { "$2a$11$Hq44m.DFvMzXk06H0zEXueWhN9KpW5neimum8TaGVWM.wzj6RscWa", "sarahbrown" });

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 5,
                columns: new[] { "PasswordHash", "Username" },
                values: new object[] { "$2a$11$Hq44m.DFvMzXk06H0zEXueWhN9KpW5neimum8TaGVWM.wzj6RscWa", "davidd" });

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: 1,
                columns: new[] { "PasswordHash", "Username" },
                values: new object[] { "$2a$11$tTnH2lAS9OQ1pkfyiay/.eW5IuLhK8FpiqH8LqFVYQcbrgOwOPjN2", "alice.martinez" });

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: 2,
                columns: new[] { "PasswordHash", "Username" },
                values: new object[] { "$2a$11$tTnH2lAS9OQ1pkfyiay/.eW5IuLhK8FpiqH8LqFVYQcbrgOwOPjN2", "bob.thompson" });

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: 3,
                columns: new[] { "PasswordHash", "Username" },
                values: new object[] { "$2a$11$tTnH2lAS9OQ1pkfyiay/.eW5IuLhK8FpiqH8LqFVYQcbrgOwOPjN2", "carol.anderson" });

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: 4,
                columns: new[] { "PasswordHash", "Username" },
                values: new object[] { "$2a$11$tTnH2lAS9OQ1pkfyiay/.eW5IuLhK8FpiqH8LqFVYQcbrgOwOPjN2", "daniel.wilson" });

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: 5,
                columns: new[] { "PasswordHash", "Username" },
                values: new object[] { "$2a$11$tTnH2lAS9OQ1pkfyiay/.eW5IuLhK8FpiqH8LqFVYQcbrgOwOPjN2", "emma.taylor" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "Username",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "Username",
                table: "Customers");
        }
    }
}
