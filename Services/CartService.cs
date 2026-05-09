using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using System.Text.Json;

namespace BlazorPizzeria.Services;

public class CartItem
{
    public string ProductType { get; set; } = string.Empty;
    public int ProductId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? SelectedSize { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}

public class CartService
{
    private readonly ProtectedSessionStorage _storage;
    private List<CartItem> _items = new();
    private bool _isInitialized = false;

    public event Action? OnChange;

    public CartService(ProtectedSessionStorage storage)
    {
        _storage = storage;
    }

    public IReadOnlyList<CartItem> Items => _items;
    public int TotalCount => _items.Sum(i => i.Quantity);
    public decimal TotalPrice => _items.Sum(i => i.Price * i.Quantity);

    public int GetQuantity(string productType, int productId, string? size = null)
    {
        var item = _items.FirstOrDefault(i => i.ProductType == productType && i.ProductId == productId && i.SelectedSize == size);
        return item?.Quantity ?? 0;
    }

    public async Task LoadCartAsync()
    {
        if (_isInitialized) return;
        try
        {
            var result = await _storage.GetAsync<string>("cart");
            if (result.Success && !string.IsNullOrEmpty(result.Value))
            {
                _items = JsonSerializer.Deserialize<List<CartItem>>(result.Value) ?? new List<CartItem>();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка загрузки корзины: {ex.Message}");
        }
        _isInitialized = true;
        OnChange?.Invoke();
    }

    private async Task SaveCartAsync()
    {
        var json = JsonSerializer.Serialize(_items);
        await _storage.SetAsync("cart", json);
    }

    public void AddItem(string productType, int productId, string name, decimal price, int quantity = 1, string? size = null)
    {
        var existing = _items.FirstOrDefault(i => i.ProductType == productType && i.ProductId == productId && i.SelectedSize == size);
        if (existing != null)
            existing.Quantity += quantity;
        else
            _items.Add(new CartItem { ProductType = productType, ProductId = productId, Name = name, SelectedSize = size, Price = price, Quantity = quantity });
        OnChange?.Invoke();
        _ = SaveCartAsync();
    }

    public void IncreaseQuantity(string productType, int productId, string? size = null)
    {
        var item = _items.FirstOrDefault(i => i.ProductType == productType && i.ProductId == productId && i.SelectedSize == size);
        if (item != null)
        {
            item.Quantity++;
            OnChange?.Invoke();
            _ = SaveCartAsync();
        }
    }

    public void DecreaseQuantity(string productType, int productId, string? size = null)
    {
        var item = _items.FirstOrDefault(i => i.ProductType == productType && i.ProductId == productId && i.SelectedSize == size);
        if (item != null)
        {
            if (item.Quantity > 1)
                item.Quantity--;
            else
                _items.Remove(item);
            OnChange?.Invoke();
            _ = SaveCartAsync();
        }
    }

    public void RemoveItem(string productType, int productId, string? size = null)
    {
        var removed = _items.RemoveAll(i => i.ProductType == productType && i.ProductId == productId && i.SelectedSize == size) > 0;
        if (removed) OnChange?.Invoke();
        _ = SaveCartAsync();
    }

    public void Clear()
    {
        if (_items.Count > 0)
        {
            _items.Clear();
            OnChange?.Invoke();
            _ = SaveCartAsync();
        }
    }
}