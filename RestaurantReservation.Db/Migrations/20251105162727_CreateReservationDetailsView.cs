using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestaurantReservation.Db.Migrations
{
    /// <inheritdoc />
    public partial class CreateReservationDetailsView : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                CREATE VIEW vw_ReservationDetails AS
                SELECT 
                    r.ReservationId,
                    r.ReservationDate,
                    r.PartySize,
                    r.CustomerId,
                    c.FirstName AS CustomerFirstName,
                    c.LastName AS CustomerLastName,
                    c.Email AS CustomerEmail,
                    c.PhoneNumber AS CustomerPhoneNumber,
                    r.RestaurantId,
                    rest.Name AS RestaurantName,
                    rest.Address AS RestaurantAddress,
                    rest.PhoneNumber AS RestaurantPhoneNumber,
                    rest.OpeningHours AS RestaurantOpeningHours
                FROM Reservations r
                INNER JOIN Customers c ON r.CustomerId = c.CustomerId
                INNER JOIN Restaurants rest ON r.RestaurantId = rest.RestaurantId
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP VIEW IF EXISTS vw_ReservationDetails");
        }
    }
}
