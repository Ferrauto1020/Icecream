using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace IcecreamApp.Api.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Icecreams",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Price = table.Column<double>(type: "double precision", nullable: false),
                    Image = table.Column<string>(type: "character varying(180)", maxLength: 180, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Icecreams", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OrderAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uuid", nullable: false),
                    CustomerName = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    CustomerEmail = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    CustomerAddress = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    TotalPrice = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    Email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Address = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Salt = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Hash = table.Column<string>(type: "character varying(180)", maxLength: 180, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IcecreamOptions",
                columns: table => new
                {
                    IcecreamId = table.Column<int>(type: "integer", nullable: false),
                    Flavor = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Topping = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IcecreamOptions", x => new { x.IcecreamId, x.Flavor, x.Topping });
                    table.ForeignKey(
                        name: "FK_IcecreamOptions_Icecreams_IcecreamId",
                        column: x => x.IcecreamId,
                        principalTable: "Icecreams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OrderId = table.Column<long>(type: "bigint", nullable: false),
                    IcecreamId = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Price = table.Column<double>(type: "double precision", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    Flavor = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Topping = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    TotalPrice = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Icecreams",
                columns: new[] { "Id", "Image", "Name", "Price" },
                values: new object[,]
                {
                    { 1, "https://raw.githubusercontent.com/Abhayprince/Images-Icons/main/Icecreams/small/ic_0.jpg", "Vanilla", 6.2000000000000002 },
                    { 2, "https://raw.githubusercontent.com/Abhayprince/Images-Icons/main/Icecreams/small/ic_1.jpg", "Chocolate", 3.5899999999999999 },
                    { 3, "https://raw.githubusercontent.com/Abhayprince/Images-Icons/main/Icecreams/small/ic_2.jpg", "Strawberry", 12.99 },
                    { 4, "https://raw.githubusercontent.com/Abhayprince/Images-Icons/main/Icecreams/small/ic_3.jpg", "Mint", 7.9900000000000002 },
                    { 5, "https://raw.githubusercontent.com/Abhayprince/Images-Icons/main/Icecreams/small/ic_4.jpg", "Caramel", 2.9900000000000002 },
                    { 6, "https://raw.githubusercontent.com/Abhayprince/Images-Icons/main/Icecreams/small/ic_5.jpg", "Pistachio", 5.29 },
                    { 7, "https://raw.githubusercontent.com/Abhayprince/Images-Icons/main/Icecreams/small/ic_6.jpg", "Cookie Dough", 9.9900000000000002 },
                    { 8, "https://raw.githubusercontent.com/Abhayprince/Images-Icons/main/Icecreams/small/ic_7.jpg", "Lemon", 3.4500000000000002 },
                    { 9, "https://raw.githubusercontent.com/Abhayprince/Images-Icons/main/Icecreams/small/ic_8.jpg", "Coffee", 7.5 },
                    { 10, "https://raw.githubusercontent.com/Abhayprince/Images-Icons/main/Icecreams/small/ic_9.jpg", "Coconut", 3.6899999999999999 },
                    { 11, "https://raw.githubusercontent.com/Abhayprince/Images-Icons/main/Icecreams/small/ic_10.jpg", "Mango", 1.0900000000000001 },
                    { 12, "https://raw.githubusercontent.com/Abhayprince/Images-Icons/main/Icecreams/small/ic_11.jpg", "Blueberry", 14.99 },
                    { 13, "https://raw.githubusercontent.com/Abhayprince/Images-Icons/main/Icecreams/small/ic_12.jpg", "Raspberry", 5.0 },
                    { 14, "https://raw.githubusercontent.com/Abhayprince/Images-Icons/main/Icecreams/small/ic_13.jpg", "Butter Pecan", 6.5899999999999999 },
                    { 15, "https://raw.githubusercontent.com/Abhayprince/Images-Icons/main/Icecreams/small/ic_14.jpg", "Peach", 1.99 },
                    { 16, "https://raw.githubusercontent.com/Abhayprince/Images-Icons/main/Icecreams/small/ic_15.jpg", "Cookies and Cream", 3.3399999999999999 },
                    { 17, "https://raw.githubusercontent.com/Abhayprince/Images-Icons/main/Icecreams/small/ic_16.jpg", "Rocky Road", 5.4199999999999999 },
                    { 18, "https://raw.githubusercontent.com/Abhayprince/Images-Icons/main/Icecreams/small/ic_17.jpg", "Cherry", 2.4900000000000002 },
                    { 19, "https://raw.githubusercontent.com/Abhayprince/Images-Icons/main/Icecreams/small/ic_18.jpg", "Blackberry", 3.9900000000000002 },
                    { 20, "https://raw.githubusercontent.com/Abhayprince/Images-Icons/main/Icecreams/small/ic_19.jpg", "Tiramisu", 4.9900000000000002 }
                });

            migrationBuilder.InsertData(
                table: "IcecreamOptions",
                columns: new[] { "Flavor", "IcecreamId", "Topping" },
                values: new object[,]
                {
                    { "Vanilla", 1, "Caramel Drizzle" },
                    { "Vanilla", 1, "Chocolate Chips" },
                    { "Chocolate", 2, "Whipped Cream" },
                    { "Strawberry", 3, "Sprinkles" },
                    { "Strawberry", 3, "White Chocolate" },
                    { "Mint", 4, "Chocolate Sauce" },
                    { "Mint", 4, "Oreo Crumbles" },
                    { "Caramel", 5, "Pecans" },
                    { "Caramel", 5, "Sea Salt" },
                    { "Pistachio", 6, "Almonds" },
                    { "Cookie Dough", 7, "Caramel Drizzle" },
                    { "Cookie Dough", 7, "Chocolate Chips" },
                    { "Lemon", 8, "Coconut Flakes" },
                    { "Coffee", 9, "Cinnamon" },
                    { "Coffee", 9, "Hazelnuts" },
                    { "Coconut", 10, "Pineapple" },
                    { "Mango", 11, "Passionfruit Sauce" },
                    { "Blueberry", 12, "Granola" },
                    { "Blueberry", 12, "White Chocolate" },
                    { "Raspberry", 13, "Dark Chocolate" },
                    { "Raspberry", 13, "White Chocolate" },
                    { "Butter Pecan", 14, "Crushed Nuts" },
                    { "Butter Pecan", 14, "Maple Syrup" },
                    { "Peach", 15, "Crushed Graham Crackers" },
                    { "Cookies and Cream", 16, "Caramel Sauce" },
                    { "Cookies and Cream", 16, "Chocolate Sauce" },
                    { "Rocky Road", 17, "Mini Marshmallows" },
                    { "Cherry", 18, "Crushed Almonds" },
                    { "Blackberry", 19, "Walnuts" },
                    { "Tiramisu", 20, "Cocoa Powder" },
                    { "Tiramisu", 20, "Espresso Drizzle" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderId",
                table: "OrderItems",
                column: "OrderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IcecreamOptions");

            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Icecreams");

            migrationBuilder.DropTable(
                name: "Orders");
        }
    }
}
