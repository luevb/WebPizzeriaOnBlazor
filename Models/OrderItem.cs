namespace BlazorPizzeria.Models;

public class OrderItem
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public Order Order { get; set; } = null!;

    public int? PizzaId { get; set; }
    public Pizza? Pizza { get; set; }

    public int? DrinkId { get; set; }
    public Drink? Drink { get; set; }

    public int? DessertId { get; set; }
    public Dessert? Dessert { get; set; }

    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }

    // Вспомогательное свойство для отображения названия продукта
    public string ProductName => Pizza?.Name ?? Drink?.Name ?? Dessert?.Name ?? "Товар";
}