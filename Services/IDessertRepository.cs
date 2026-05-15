using BlazorPizzeria.Models;

namespace BlazorPizzeria.Services;

public interface IDessertRepository
{
    Task<List<Dessert>> GetAllAsync();
    Task<Dessert?> GetByIdAsync(int id);
}