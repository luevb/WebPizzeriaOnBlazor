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

    public async Task<List<Order>> GetAllAsync()
    {
        await using var db = await _dbFactory.CreateDbContextAsync();
        return await db.Orders
            .Include(o => o.OrderItems)
            .ThenInclude(oi => oi.Pizza)
            .Include(o => o.OrderItems)
            .ThenInclude(oi => oi.Drink)
            .Include(o => o.OrderItems)
            .ThenInclude(oi => oi.Dessert)
            .OrderByDescending(o => o.Id)
            .ToListAsync();
    }

    public async Task<Order?> GetByIdAsync(int id)
    {
        await using var db = await _dbFactory.CreateDbContextAsync();
        return await db.Orders
            .Include(o => o.OrderItems)
            .FirstOrDefaultAsync(o => o.Id == id);
    }

    public async Task AddAsync(Order order)
    {
        await using var db = await _dbFactory.CreateDbContextAsync();
        db.Orders.Add(order);
        await db.SaveChangesAsync();
    }

    public async Task UpdateStatusAsync(int id, string status)
    {
        await using var db = await _dbFactory.CreateDbContextAsync();
        var order = await db.Orders.FindAsync(id);
        if (order != null)
        {
            order.Status = status;
            await db.SaveChangesAsync();
        }
    }

    public async Task<List<Order>> GetFilteredOrdersAsync(string? status, DateTime? fromDate, DateTime? toDate, string? searchTerm)
    {
        await using var db = await _dbFactory.CreateDbContextAsync();
        var query = db.Orders
            .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Pizza)
            .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Drink)
            .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Dessert)
            .AsQueryable();

        if (!string.IsNullOrEmpty(status) && status != "All")
            query = query.Where(o => o.Status == status);

        if (fromDate.HasValue)
            query = query.Where(o => o.OrderDate.Date >= fromDate.Value.Date);
        if (toDate.HasValue)
            query = query.Where(o => o.OrderDate.Date <= toDate.Value.Date);

        if (!string.IsNullOrEmpty(searchTerm))
        {
            // поиск по ID или телефону
            if (int.TryParse(searchTerm, out int id))
                query = query.Where(o => o.Id == id);
            else
                query = query.Where(o => o.Phone.Contains(searchTerm));
        }

        return await query.OrderByDescending(o => o.Id).ToListAsync();
    }
}