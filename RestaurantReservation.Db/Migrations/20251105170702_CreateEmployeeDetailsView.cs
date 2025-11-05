using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestaurantReservation.Db.Migrations
{
    /// <inheritdoc />
    public partial class CreateEmployeeDetailsView : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                CREATE VIEW vw_EmployeeDetails AS
                SELECT 
                    e.EmployeeId,
                    e.FirstName,
                    e.LastName,
                    e.Position,
                    e.RestaurantId,
                    r.Name AS RestaurantName,
                    r.Address AS RestaurantAddress,
                    r.PhoneNumber AS RestaurantPhoneNumber,
                    r.OpeningHours AS RestaurantOpeningHours
                FROM Employees e
                INNER JOIN Restaurants r ON e.RestaurantId = r.RestaurantId
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP VIEW IF EXISTS vw_EmployeeDetails");
        }
    }
}
