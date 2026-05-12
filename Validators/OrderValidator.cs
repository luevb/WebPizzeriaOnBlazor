using FluentValidation;
using BlazorPizzeria.Models;

namespace BlazorPizzeria.Validators;

public class OrderValidator : AbstractValidator<Order>
{
    public OrderValidator()
    {
        RuleFor(o => o.CustomerName)
            .NotEmpty().WithMessage("Введите ваше имя")
            .Length(2, 100).WithMessage("Имя должно быть от 2 до 100 символов");

        RuleFor(o => o.Phone)
            .NotEmpty().WithMessage("Введите номер телефона")
            .Must(BeValidPhone).WithMessage("Введите корректный номер телефона (10-15 цифр)");

        RuleFor(o => o.Address)
            .Must((order, address) => BeValidAddressOrder(order, address))
            .WithMessage("Укажите полный адрес (улица, дом)");
    }

    private bool BeValidPhone(string phone)
    {
        if (string.IsNullOrWhiteSpace(phone)) return false;
        var digits = new string(phone.Where(char.IsDigit).ToArray());
        return digits.Length >= 10 && digits.Length <= 15;
    }

    private bool BeValidAddressOrder(Order order, string address)
    {
        if (order.DeliveryType == "Pickup") return true;
        return !string.IsNullOrWhiteSpace(address) && address.Any(char.IsDigit) && address.Any(char.IsLetter);
    }
}