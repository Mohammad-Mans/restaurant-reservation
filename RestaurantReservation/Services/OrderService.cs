using RestaurantReservation.Db.Interfaces;
using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Services;

public class OrderService(IOrderRepository repository)
{
    public Order CreateOrder(int reservationId, int employeeId, DateTime orderDate, decimal totalAmount)
    {
        var order = new Order
        {
            ReservationId = reservationId,
            EmployeeId = employeeId,
            OrderDate = orderDate,
            TotalAmount = totalAmount
        };
        return repository.Create(order);
    }

    public Order? UpdateOrder(int id, int? reservationId = null, int? employeeId = null, DateTime? orderDate = null,
        decimal? totalAmount = null)
    {
        var order = repository.GetById(id);
        if (order == null) return null;

        if (reservationId.HasValue) order.ReservationId = reservationId.Value;
        if (employeeId.HasValue) order.EmployeeId = employeeId.Value;
        if (orderDate.HasValue) order.OrderDate = orderDate.Value;
        if (totalAmount.HasValue) order.TotalAmount = totalAmount.Value;

        return repository.Update(order);
    }

    public bool DeleteOrder(int id)
    {
        return repository.Delete(id);
    }

    public List<Order> ListOrdersAndMenuItems(int reservationId)
    {
        return repository.GetByReservationId(reservationId);
    }
}