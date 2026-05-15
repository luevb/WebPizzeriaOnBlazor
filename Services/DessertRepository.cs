using BlazorPizzeria.Data;
using BlazorPizzeria.Models;
using Microsoft.EntityFrameworkCore;

namespace BlazorPizzeria.Services;

public class DessertRepository : IDessertRepository
{
    private readonly IDbContextFactory<AppDbContext> _dbFactory;

    public DessertRepository(IDbContextFactory<AppDbContext> dbFactory)
    {
        _dbFactory = dbFactory;
    }

    public async Task<List<Dessert>> GetAllAsync()
    {
        await using var db = await _dbFactory.CreateDbContextAsync();
        return await db.Desserts.ToListAsync();
    }

    public async Task<Dessert?> GetByIdAsync(int id)
    {
        await using var db = await _dbFactory.CreateDbContextAsync();
        return await db.Desserts.FindAsync(id);
    }
}