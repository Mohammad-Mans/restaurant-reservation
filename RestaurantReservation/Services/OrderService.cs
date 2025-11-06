using RestaurantReservation.Db.Interfaces;
using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Services;

public class OrderService(IOrderRepository repository)
{
    public async Task<Order> CreateOrderAsync(int reservationId, int employeeId, DateTime orderDate, decimal totalAmount)
    {
        var order = new Order
        {
            ReservationId = reservationId,
            EmployeeId = employeeId,
            OrderDate = orderDate,
            TotalAmount = totalAmount
        };
        return await repository.CreateAsync(order);
    }

    public async Task<Order?> UpdateOrderAsync(int id, int? reservationId = null, int? employeeId = null, DateTime? orderDate = null,
        decimal? totalAmount = null)
    {
        var order = await repository.GetByIdAsync(id);
        if (order == null) return null;

        if (reservationId.HasValue) order.ReservationId = reservationId.Value;
        if (employeeId.HasValue) order.EmployeeId = employeeId.Value;
        if (orderDate.HasValue) order.OrderDate = orderDate.Value;
        if (totalAmount.HasValue) order.TotalAmount = totalAmount.Value;

        return await repository.UpdateAsync(order);
    }

    public async Task<bool> DeleteOrderAsync(int id)
    {
        return await repository.DeleteAsync(id);
    }

    public async Task<List<Order>> ListOrdersAndMenuItemsAsync(int reservationId)
    {
        return await repository.GetByReservationIdAsync(reservationId);
    }

    public async Task<decimal?> CalculateAverageOrderAmountAsync(int employeeId)
    {
        return await repository.GetAverageOrderAmountByEmployeeIdAsync(employeeId);
    }
}