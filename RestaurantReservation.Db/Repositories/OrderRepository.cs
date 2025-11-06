using Microsoft.EntityFrameworkCore;
using RestaurantReservation.Db.Interfaces;
using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Db.Repositories;

public class OrderRepository(RestaurantReservationDbContext context) : IOrderRepository
{
    public async Task<Order> CreateAsync(Order order)
    {
        context.Orders.Add(order);
        await context.SaveChangesAsync();
        return order;
    }

    public async Task<Order?> GetByIdAsync(int id)
    {
        return await context.Orders.FindAsync(id);
    }

    public async Task<List<Order>> GetAllAsync()
    {
        return await context.Orders.ToListAsync();
    }

    public async Task<Order?> UpdateAsync(Order order)
    {
        var existing = await context.Orders.FindAsync(order.OrderId);
        if (existing == null) return null;

        existing.ReservationId = order.ReservationId;
        existing.EmployeeId = order.EmployeeId;
        existing.OrderDate = order.OrderDate;
        existing.TotalAmount = order.TotalAmount;

        await context.SaveChangesAsync();
        return existing;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var order = await context.Orders.FindAsync(id);
        if (order == null) return false;

        context.Orders.Remove(order);
        await context.SaveChangesAsync();
        return true;
    }

    public async Task<List<Order>> GetByReservationIdAsync(int reservationId)
    {
        return await context.Orders
            .Where(o => o.ReservationId == reservationId)
            .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Item)
            .ToListAsync();
    }

    public async Task<decimal?> GetAverageOrderAmountByEmployeeIdAsync(int employeeId)
    {
        var employeeOrders = context.Orders.Where(o => o.EmployeeId == employeeId);

        if (!await employeeOrders.AnyAsync())
            return null;

        return await employeeOrders.AverageAsync(o => o.TotalAmount);
    }
}