using FluentValidation;
using BlazorPizzeria.Models;
using System.Text.RegularExpressions;

namespace BlazorPizzeria.Validators;

public class OrderValidator : AbstractValidator<Order>
{
    private static readonly Regex PhoneRegex = new(@"^\+7\d{10}$", RegexOptions.Compiled);
    private static readonly Regex NameRegex = new(@"^[a-zA-Zа-яА-ЯёЁ\s\-']+$", RegexOptions.Compiled);

    public OrderValidator()
    {
        RuleFor(o => o.CustomerName)
            .NotEmpty().WithMessage("Введите ваше имя")
            .Length(2, 100).WithMessage("Имя должно содержать от 2 до 100 символов")
            .Matches(NameRegex).WithMessage("Имя может содержать только буквы, пробелы, дефис и апостроф");

        RuleFor(o => o.Phone)
            .NotEmpty().WithMessage("Введите номер телефона")
            .Matches(PhoneRegex).WithMessage("Номер телефона должен быть в формате +7XXXXXXXXXX (10 цифр после +7)");

        RuleFor(o => o.Address)
            .Must((order, address) => order.DeliveryType != "Delivery" || !string.IsNullOrWhiteSpace(address))
            .WithMessage("Укажите адрес доставки");
    }
}