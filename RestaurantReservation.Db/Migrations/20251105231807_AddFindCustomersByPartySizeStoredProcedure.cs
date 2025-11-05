using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestaurantReservation.Db.Migrations
{
    /// <inheritdoc />
    public partial class AddFindCustomersByPartySizeStoredProcedure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                CREATE PROCEDURE dbo.sp_FindCustomersByPartySize
                    @MinPartySize INT
                AS
                BEGIN
                    SET NOCOUNT ON;
                    
                    SELECT DISTINCT
                        c.CustomerId,
                        c.FirstName,
                        c.LastName,
                        c.Email,
                        c.PhoneNumber
                    FROM Customers c
                    INNER JOIN Reservations r ON c.CustomerId = r.CustomerId
                    WHERE r.PartySize > @MinPartySize
                    ORDER BY c.LastName, c.FirstName;
                END
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS dbo.sp_FindCustomersByPartySize");
        }
    }
}
