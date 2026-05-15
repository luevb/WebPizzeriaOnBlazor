using BlazorPizzeria.Models;

namespace BlazorPizzeria.Services;

public interface IDrinkRepository
{
    Task<List<Drink>> GetAllAsync();
    Task<Drink?> GetByIdAsync(int id);
}