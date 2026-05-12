using BlazorPizzeria.Data;
using BlazorPizzeria.Models;
using Microsoft.EntityFrameworkCore;

namespace BlazorPizzeria.Services;

public class PizzaRepository : IPizzaRepository
{
    private readonly IDbContextFactory<AppDbContext> _dbFactory;

    public PizzaRepository(IDbContextFactory<AppDbContext> dbFactory)
    {
        _dbFactory = dbFactory;
    }

    public async Task<List<Pizza>> GetAllAsync()
    {
        await using var db = await _dbFactory.CreateDbContextAsync();
        return await db.Pizzas.ToListAsync();
    }

    public async Task<Pizza?> GetByIdAsync(int id)
    {
        await using var db = await _dbFactory.CreateDbContextAsync();
        return await db.Pizzas.FindAsync(id);
    }

    public async Task AddAsync(Pizza pizza)
    {
        await using var db = await _dbFactory.CreateDbContextAsync();
        db.Pizzas.Add(pizza);
        await db.SaveChangesAsync();
    }

    public async Task UpdateAsync(Pizza pizza)
    {
        await using var db = await _dbFactory.CreateDbContextAsync();
        db.Pizzas.Update(pizza);
        await db.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        await using var db = await _dbFactory.CreateDbContextAsync();
        var pizza = await db.Pizzas.FindAsync(id);
        if (pizza != null)
        {
            db.Pizzas.Remove(pizza);
            await db.SaveChangesAsync();
        }
    }
}