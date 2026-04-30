using BlazorPizzeria.Models;

namespace BlazorPizzeria.Services;

public interface IOrderRepository
{
    Task<List<Order>> GetAllWithItemsAsync();
    Task<Order?> GetByIdAsync(int id);
    Task UpdateStatusAsync(int orderId, string status);
}