using BlazorPizzeria.Models;

namespace BlazorPizzeria.Data
{
    public class Initializer
    {
        public static List<Pizza> GetPizzas()
        {
                    return new List<Pizza>
            {
                new Pizza
                {
                    Id = 1,
                    Name = "Маргарита",
                    Description = "Томатный соус, свежая моцарелла, душистый базилик, оливковое масло extra virgin.",
                    Price = 490m,
                    Size = "Medium",
                    IsVegetarian = true,
                    ImageUrl = "/images/pizzas/margherita.jpg"
                },
                new Pizza
                {
                    Id = 2,
                    Name = "Четыре сыра",
                    Description = "Сливочный соус и смесь из моцареллы, пармезана, горгонзолы, фонтана.",
                    Price = 620m,
                    Size = "Medium",
                    IsVegetarian = true,
                    ImageUrl = "/images/pizzas/quattro_formaggi.jpg"
                },
                new Pizza
                {
                    Id = 3,
                    Name = "Четыре сезона",
                    Description = "Артишоки (весна), оливки и томаты (лето), прошутто или грибы (осень), моцарелла (зима).",
                    Price = 680m,
                    Size = "Medium",
                    IsVegetarian = false,
                    ImageUrl = "/images/pizzas/quattro_stagioni.jpg"
                },
                new Pizza
                {
                    Id = 4,
                    Name = "Капричоза",
                    Description = "Моцарелла, томаты, прошутто или ветчина, артишоки, шампиньоны, оливки.",
                    Price = 670m,
                    Size = "Medium",
                    IsVegetarian = false,
                    ImageUrl = "/images/pizzas/capricciosa.jpg"
                },

                new Pizza
                {
                    Id = 5,
                    Name = "Дьябола",
                    Description = "Салями, халапеньо, томатный соус, сыр — дьявольски острая.",
                    Price = 650m,
                    Size = "Medium",
                    IsVegetarian = false,
                    ImageUrl = "/images/pizzas/diavola.jpg"
                },
                new Pizza
                {
                    Id = 6,
                    Name = "Гавайская",
                    Description = "Курица, сыр и сочные кусочки ананаса — сладко-солёная классика.",
                    Price = 610m,
                    Size = "Medium",
                    IsVegetarian = false,
                    ImageUrl = "/images/pizzas/hawaiian.jpg"
                },
                new Pizza
                {
                    Id = 7,
                    Name = "Вегетарианская",
                    Description = "Сладкий перец, цуккини, баклажаны, шампиньоны, томаты, маслины — полезно и сытно.",
                    Price = 560m,
                    Size = "Medium",
                    IsVegetarian = true,
                    ImageUrl = "/images/pizzas/vegetariana.jpg"
                },
                new Pizza
                {
                    Id = 8,
                    Name = "Кальцоне",
                    Description = "Закрытая пицца с рикоттой, моцареллой, прошутто или грибами в тонком хрустящем тесте.",
                    Price = 640m,
                    Size = "Medium",
                    IsVegetarian = false,
                    ImageUrl = "/images/pizzas/calzone.jpg"
                },
                new Pizza
                {
                    Id = 9,
                    Name = "Пицца Дон (фирменная)",
                    Description = "Вяленые томаты, пармезан, копчёная паприка — наш секретный рецепт.",
                    Price = 720m,
                    Size = "Medium",
                    IsVegetarian = false,
                    ImageUrl = "/images/pizzas/signature.jpg"
                },
                new Pizza
                {
                    Id = 10,
                    Name = "Пармская",
                    Description = "Прошутто крудо, пармезан, руккола, вяленые томаты, бальзамический крем.",
                    Price = 750m,
                    Size = "Medium",
                    IsVegetarian = false,
                    ImageUrl = "/images/pizzas/parmense.jpg"
                }
            };
        }
        public static List<Drink> GetDrinks()
        {
                return new List<Drink>
            {
                new Drink { Id = 1, Name = "Кока-кола", Description = "0.5 л", Price = 150m, ImageUrl = "/images/drinks/coke.jpg" },
                new Drink { Id = 2, Name = "Морс", Description = "Домашний", Price = 120m, ImageUrl = "/images/drinks/mors.jpg" },
                new Drink { Id = 3, Name = "Спрайт", Description = "0.5 л", Price = 150m, ImageUrl = "/images/drinks/sprite.jpg" },
                new Drink { Id = 4, Name = "Фанта", Description = "0.5 л", Price = 150m, ImageUrl = "/images/drinks/fanta.jpg" },
                new Drink { Id = 5, Name = "Адреналин", Description = "0.449 л", Price = 190m, ImageUrl = "/images/drinks/adrenaline.jpg" },
                new Drink { Id = 6, Name = "Вода", Description = "0.5 л", Price = 90m, ImageUrl = "/images/drinks/water.jpg" },
            };
        }

        public static List<Dessert> GetDesserts()
        {
            return new List<Dessert>
        {
            new Dessert { Id = 1, Name = "Тирамису", Description = "Классический", Price = 380m, Calories = 450, ImageUrl = "/images/desserts/tiramisu.jpg" },
            new Dessert { Id = 2, Name = "Чизкейк", Description = "Нью-Йорк", Price = 350m, Calories = 420, ImageUrl = "/images/desserts/cheesecake.jpg" },
        };
        }
    }
}
