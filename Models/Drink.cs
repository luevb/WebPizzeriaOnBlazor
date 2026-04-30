namespace BlazorPizzeria.Models;

public class Drink
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string? ImageUrl { get; set; }

    public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}