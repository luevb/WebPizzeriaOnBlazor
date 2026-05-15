using Microsoft.EntityFrameworkCore;
using BlazorPizzeria.Models;

namespace BlazorPizzeria.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Pizza> Pizzas { get; set; }
    public DbSet<Drink> Drinks { get; set; }
    public DbSet<Dessert> Desserts { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        // Настройка OrderItem: три внешних ключа, только один может быть ненулевым
        modelBuilder.Entity<OrderItem>()
            .HasOne(oi => oi.Pizza)
            .WithMany(p => p.OrderItems)
            .HasForeignKey(oi => oi.PizzaId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<OrderItem>()
            .HasOne(oi => oi.Drink)
            .WithMany(d => d.OrderItems)
            .HasForeignKey(oi => oi.DrinkId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<OrderItem>()
            .HasOne(oi => oi.Dessert)
            .WithMany(d => d.OrderItems)
            .HasForeignKey(oi => oi.DessertId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<PizzaIngredient>()
       .HasKey(pi => new { pi.PizzaId, pi.IngredientId });

        modelBuilder.Entity<PizzaIngredient>()
            .HasOne(pi => pi.Pizza)
            .WithMany(p => p.PizzaIngredients)
            .HasForeignKey(pi => pi.PizzaId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<PizzaIngredient>()
            .HasOne(pi => pi.Ingredient)
            .WithMany(i => i.PizzaIngredients)
            .HasForeignKey(pi => pi.IngredientId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Ingredient>().HasData(
            new Ingredient { Id = 1, Name = "Сыр моцарелла", ExtraPrice = 50m },
            new Ingredient { Id = 2, Name = "Пепперони", ExtraPrice = 70m },
            new Ingredient { Id = 3, Name = "Грибы", ExtraPrice = 40m }
        );

        modelBuilder.Entity<PizzaIngredient>().HasData(
            new PizzaIngredient { PizzaId = 1, IngredientId = 1 }
        );

        // Точность для цен
        modelBuilder.Entity<Pizza>().Property(p => p.Price).HasPrecision(18, 2);
        modelBuilder.Entity<Drink>().Property(d => d.Price).HasPrecision(18, 2);
        modelBuilder.Entity<Dessert>().Property(d => d.Price).HasPrecision(18, 2);
        modelBuilder.Entity<OrderItem>().Property(oi => oi.UnitPrice).HasPrecision(18, 2);
        modelBuilder.Entity<Order>().Property(o => o.TotalPrice).HasPrecision(18, 2);

        modelBuilder.Entity<Pizza>().HasData(Initializer.GetPizzas());
        modelBuilder.Entity<Drink>().HasData(Initializer.GetDrinks());
        modelBuilder.Entity<Dessert>().HasData(Initializer.GetDesserts());
    }
}