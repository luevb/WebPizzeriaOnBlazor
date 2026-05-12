using BlazorPizzeria.Models;
using System.ComponentModel.DataAnnotations;

public class Order
{
    public int Id { get; set; }
    [Required(ErrorMessage = "Укажите ваше имя")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Имя должно содержать от 2 до 100 символов")]
    public string CustomerName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Укажите телефон для связи")]
    [Phone(ErrorMessage = "Введите корректный номер телефона")]
    public string Phone { get; set; } = string.Empty;

    [Required(ErrorMessage = "Укажите адрес доставки")]
    [StringLength(200, ErrorMessage = "Адрес не может быть длиннее 200 символов")]
    public string Address { get; set; } = string.Empty;
    public DateTime OrderDate { get; set; }
    public decimal TotalPrice { get; set; }
    public string DeliveryType { get; set; } = "Delivery"; // "Delivery" или "Pickup"
    public string Status { get; set; } = "Pending";

    public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}