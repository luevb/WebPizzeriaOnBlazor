using BlazorPizzeria.Models;

public class Pizza
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string Size { get; set; } = "Medium";
    public bool IsVegetarian { get; set; }
    public string? ImageUrl { get; set; }

    public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    public ICollection<PizzaIngredient> PizzaIngredients { get; set; } = new List<PizzaIngredient>();
}