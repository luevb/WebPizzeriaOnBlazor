namespace BlazorPizzeria.Services;

/// <summary>
/// Представляет один товар в корзине.
/// </summary>
public class CartItem
{
    public string ProductType { get; set; } = string.Empty; // "Pizza", "Drink", "Dessert"
    public int ProductId { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}

/// <summary>
/// Сервис корзины товаров. Хранит список выбранных товаров и предоставляет методы для управления.
/// </summary>
public class CartService
{
    private List<CartItem> _items = new();

    /// <summary>
    /// Событие, вызываемое при любом изменении содержимого корзины.
    /// Подписывайтесь на него, чтобы обновлять интерфейс.
    /// </summary>
    public event Action? OnChange;

    /// <summary>
    /// Возвращает все позиции в корзине (только для чтения).
    /// </summary>
    public IReadOnlyList<CartItem> Items => _items;

    /// <summary>
    /// Общее количество товаров (сумма всех Quantity).
    /// </summary>
    public int TotalCount => _items.Sum(i => i.Quantity);

    /// <summary>
    /// Общая стоимость корзины.
    /// </summary>
    public decimal TotalPrice => _items.Sum(i => i.Price * i.Quantity);

    /// <summary>
    /// Возвращает количество единиц указанного товара в корзине.
    /// </summary>
    /// <param name="productType">Тип продукта (Pizza, Drink, Dessert)</param>
    /// <param name="productId">Идентификатор продукта</param>
    public int GetQuantity(string productType, int productId)
    {
        var item = _items.FirstOrDefault(i => i.ProductType == productType && i.ProductId == productId);
        return item?.Quantity ?? 0;
    }

    /// <summary>
    /// Добавляет указанное количество товара в корзину. Если товар уже есть, увеличивает количество.
    /// </summary>
    public void AddItem(string productType, int productId, string name, decimal price, int quantity = 1)
    {
        var existing = _items.FirstOrDefault(i => i.ProductType == productType && i.ProductId == productId);
        if (existing != null)
            existing.Quantity += quantity;
        else
            _items.Add(new CartItem { ProductType = productType, ProductId = productId, Name = name, Price = price, Quantity = quantity });

        OnChange?.Invoke(); // Уведомляем об изменении
    }

    /// <summary>
    /// Увеличивает количество товара на 1.
    /// </summary>
    public void IncreaseQuantity(string productType, int productId)
    {
        var item = _items.FirstOrDefault(i => i.ProductType == productType && i.ProductId == productId);
        if (item != null)
        {
            item.Quantity++;
            OnChange?.Invoke();
        }
    }

    /// <summary>
    /// Уменьшает количество товара на 1. Если количество становится 0, товар полностью удаляется из корзины.
    /// </summary>
    public void DecreaseQuantity(string productType, int productId)
    {
        var item = _items.FirstOrDefault(i => i.ProductType == productType && i.ProductId == productId);
        if (item != null)
        {
            if (item.Quantity > 1)
                item.Quantity--;
            else
                _items.Remove(item);
            OnChange?.Invoke();
        }
    }

    /// <summary>
    /// Полностью удаляет указанный товар из корзины (независимо от его количества).
    /// </summary>
    public void RemoveItem(string productType, int productId)
    {
        var removed = _items.RemoveAll(i => i.ProductType == productType && i.ProductId == productId) > 0;
        if (removed) OnChange?.Invoke();
    }

    /// <summary>
    /// Полностью очищает корзину.
    /// </summary>
    public void Clear()
    {
        if (_items.Count > 0)
        {
            _items.Clear();
            OnChange?.Invoke();
        }
    }
}