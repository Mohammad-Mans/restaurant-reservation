namespace RestaurantReservation.Db.Models;

public class Customer
{
    public int CustomerId { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName  { get; set; } = null!;
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }

    public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
}