namespace BlazorPizzeria.Models;

public class Ingredient
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal ExtraPrice { get; set; } = 0m; // надбавка за ингредиент, если нужно

    public ICollection<PizzaIngredient> PizzaIngredients { get; set; } = new List<PizzaIngredient>();
}