using BlazorPizzeria.Data;
using BlazorPizzeria.Models;
using Microsoft.EntityFrameworkCore;

namespace BlazorPizzeria.Services;

public class OrderRepository : IOrderRepository
{
    private readonly IDbContextFactory<AppDbContext> _dbFactory;

    public OrderRepository(IDbContextFactory<AppDbContext> dbFactory)
    {
        _dbFactory = dbFactory;
    }

    public async Task<List<Order>> GetAllWithItemsAsync()
    {
        await using var db = await _dbFactory.CreateDbContextAsync();
        return await db.Orders
            .AsNoTracking()
            .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Pizza)
            .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Drink)
            .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Dessert)
            .OrderByDescending(o => o.OrderDate)
            .ToListAsync();
    }

    public async Task<Order?> GetByIdAsync(int id)
    {
        await using var db = await _dbFactory.CreateDbContextAsync();
        return await db.Orders
            .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Pizza)
            .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Drink)
            .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Dessert)
            .FirstOrDefaultAsync(o => o.Id == id);
    }

    public async Task UpdateStatusAsync(int orderId, string status)
    {
        await using var db = await _dbFactory.CreateDbContextAsync();
        var order = await db.Orders.FindAsync(orderId);
        if (order != null)
        {
            order.Status = status;
            await db.SaveChangesAsync();
        }
    }
}