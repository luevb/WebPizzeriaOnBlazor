using BlazorPizzeria.Models;

namespace BlazorPizzeria.Services;

public interface IOrderRepository
{
    Task<List<Order>> GetAllAsync();
    Task<Order?> GetByIdAsync(int id);
    Task AddAsync(Order order);
    Task UpdateStatusAsync(int id, string status);
    Task<List<Order>> GetFilteredOrdersAsync(string? status, DateTime? fromDate, DateTime? toDate, string? searchTerm);
}