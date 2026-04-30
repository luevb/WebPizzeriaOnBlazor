using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BlazorPizzeria.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Desserts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    Price = table.Column<decimal>(type: "TEXT", precision: 18, scale: 2, nullable: false),
                    Calories = table.Column<int>(type: "INTEGER", nullable: false),
                    ImageUrl = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Desserts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Drinks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    Price = table.Column<decimal>(type: "TEXT", precision: 18, scale: 2, nullable: false),
                    ImageUrl = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drinks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CustomerName = table.Column<string>(type: "TEXT", nullable: false),
                    Phone = table.Column<string>(type: "TEXT", nullable: false),
                    Address = table.Column<string>(type: "TEXT", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "TEXT", precision: 18, scale: 2, nullable: false),
                    Status = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pizzas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    Price = table.Column<decimal>(type: "TEXT", precision: 18, scale: 2, nullable: false),
                    Size = table.Column<string>(type: "TEXT", nullable: false),
                    IsVegetarian = table.Column<bool>(type: "INTEGER", nullable: false),
                    ImageUrl = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pizzas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    OrderId = table.Column<int>(type: "INTEGER", nullable: false),
                    PizzaId = table.Column<int>(type: "INTEGER", nullable: true),
                    DrinkId = table.Column<int>(type: "INTEGER", nullable: true),
                    DessertId = table.Column<int>(type: "INTEGER", nullable: true),
                    Quantity = table.Column<int>(type: "INTEGER", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "TEXT", precision: 18, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItems_Desserts_DessertId",
                        column: x => x.DessertId,
                        principalTable: "Desserts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderItems_Drinks_DrinkId",
                        column: x => x.DrinkId,
                        principalTable: "Drinks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderItems_Pizzas_PizzaId",
                        column: x => x.PizzaId,
                        principalTable: "Pizzas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Desserts",
                columns: new[] { "Id", "Calories", "Description", "ImageUrl", "Name", "Price" },
                values: new object[,]
                {
                    { 1, 450, "Классический", "/images/desserts/tiramisu.jpg", "Тирамису", 380m },
                    { 2, 420, "Нью-Йорк", "/images/desserts/cheesecake.jpg", "Чизкейк", 350m }
                });

            migrationBuilder.InsertData(
                table: "Drinks",
                columns: new[] { "Id", "Description", "ImageUrl", "Name", "Price" },
                values: new object[,]
                {
                    { 1, "0.5 л", "/images/drinks/coke.jpg", "Кока-кола", 150m },
                    { 2, "Домашний", "/images/drinks/mors.jpg", "Морс", 120m },
                    { 3, "0.5 л", "/images/drinks/sprite.jpg", "Спрайт", 150m },
                    { 4, "0.5 л", "/images/drinks/fanta.jpg", "Фанта", 150m },
                    { 5, "0.449 л", "/images/drinks/adrenaline.jpg", "Адреналин", 190m },
                    { 6, "0.5 л", "/images/drinks/water.jpg", "Вода", 90m }
                });

            migrationBuilder.InsertData(
                table: "Pizzas",
                columns: new[] { "Id", "Description", "ImageUrl", "IsVegetarian", "Name", "Price", "Size" },
                values: new object[,]
                {
                    { 1, "Томатный соус, свежая моцарелла, душистый базилик, оливковое масло extra virgin.", "/images/pizzas/margherita.jpg", true, "Маргарита", 490m, "Medium" },
                    { 2, "Сливочный соус и смесь из моцареллы, пармезана, горгонзолы, фонтана.", "/images/pizzas/quattro_formaggi.jpg", true, "Четыре сыра", 620m, "Medium" },
                    { 3, "Артишоки (весна), оливки и томаты (лето), прошутто или грибы (осень), моцарелла (зима).", "/images/pizzas/quattro_stagioni.jpg", false, "Четыре сезона", 680m, "Medium" },
                    { 4, "Моцарелла, томаты, прошутто или ветчина, артишоки, шампиньоны, оливки.", "/images/pizzas/capricciosa.jpg", false, "Капричоза", 670m, "Medium" },
                    { 5, "Салями, халапеньо, томатный соус, сыр — дьявольски острая.", "/images/pizzas/diavola.jpg", false, "Дьябола", 650m, "Medium" },
                    { 6, "Курица, сыр и сочные кусочки ананаса — сладко-солёная классика.", "/images/pizzas/hawaiian.jpg", false, "Гавайская", 610m, "Medium" },
                    { 7, "Сладкий перец, цуккини, баклажаны, шампиньоны, томаты, маслины — полезно и сытно.", "/images/pizzas/vegetariana.jpg", true, "Вегетарианская", 560m, "Medium" },
                    { 8, "Закрытая пицца с рикоттой, моцареллой, прошутто или грибами в тонком хрустящем тесте.", "/images/pizzas/calzone.jpg", false, "Кальцоне", 640m, "Medium" },
                    { 9, "Вяленые томаты, пармезан, копчёная паприка — наш секретный рецепт.", "/images/pizzas/signature.jpg", false, "Пицца Дон (фирменная)", 720m, "Medium" },
                    { 10, "Прошутто крудо, пармезан, руккола, вяленые томаты, бальзамический крем.", "/images/pizzas/parmense.jpg", false, "Пармская", 750m, "Medium" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_DessertId",
                table: "OrderItems",
                column: "DessertId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_DrinkId",
                table: "OrderItems",
                column: "DrinkId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderId",
                table: "OrderItems",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_PizzaId",
                table: "OrderItems",
                column: "PizzaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "Desserts");

            migrationBuilder.DropTable(
                name: "Drinks");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Pizzas");
        }
    }
}
