using BlazorPizzeria.Data;
using BlazorPizzeria.Models;
using Microsoft.EntityFrameworkCore;

namespace BlazorPizzeria.Services;

public class DrinkRepository : IDrinkRepository
{
    private readonly IDbContextFactory<AppDbContext> _dbFactory;

    public DrinkRepository(IDbContextFactory<AppDbContext> dbFactory)
    {
        _dbFactory = dbFactory;
    }

    public async Task<List<Drink>> GetAllAsync()
    {
        await using var db = await _dbFactory.CreateDbContextAsync();
        return await db.Drinks.ToListAsync();
    }

    public async Task<Drink?> GetByIdAsync(int id)
    {
        await using var db = await _dbFactory.CreateDbContextAsync();
        return await db.Drinks.FindAsync(id);
    }
}