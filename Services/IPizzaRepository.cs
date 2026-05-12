using BlazorPizzeria.Models;

namespace BlazorPizzeria.Services;

public interface IPizzaRepository
{
    Task<List<Pizza>> GetAllAsync();
    Task<Pizza?> GetByIdAsync(int id);
    Task AddAsync(Pizza pizza);
    Task UpdateAsync(Pizza pizza);
    Task DeleteAsync(int id);
}