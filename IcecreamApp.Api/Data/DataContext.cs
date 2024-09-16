using IcecreamApp.Api.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace IcecreamApp.Api.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Icecream> Icecreams { get; set; }
        public DbSet<IcecreamOptions> IcecreamOptions { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IcecreamOptions>()
            .HasKey(io => new { io.IcecreamId, io.Flavor, io.Topping });
            AddSeedData(modelBuilder);
        }

        private static void AddSeedData(ModelBuilder modelBuilder)
        {
            Icecream[] icecreams = [
                        new Icecream { Id = 1, Name = "Vanilla", Image = "https://raw.githubusercontent.com/Abhayprince/Images-Icons/main/Icecreams/small/ic_0.jpg",Price= 6.20},
                        new Icecream { Id = 2, Name = "Chocolate", Image = "https://raw.githubusercontent.com/Abhayprince/Images-Icons/main/Icecreams/small/ic_1.jpg", Price=3.59},
                        new Icecream { Id = 3, Name = "Strawberry", Image = "https://raw.githubusercontent.com//Abhayprince/Images-Icons/main/Icecreams/small/ic_2.jpg", Price=12.99},
                        new Icecream { Id = 4, Name = "Mint", Image = "https://raw.githubusercontent.com/Abhayprince/Images-Icons/main/Icecreams/small/ic_3.jpg", Price =7.99},
                        new Icecream { Id = 5, Name = "Caramel", Image = "https://raw.githubusercontent.com//Abhayprince/Images-Icons/main/Icecreams/small/ic_4.jpg", Price =2.99},
                        new Icecream { Id = 6, Name = "Pistachio", Image = "https://raw.githubusercontent.com/Images-Icons/main/Icecreams/small/ic_5.jpg", Price =5.29},
                        new Icecream { Id = 7, Name = "Cookie Dough", Image = "https://raw.githubusercontent.com/Abhayprince/Images-Icons/main/Icecreams/small/ic_6.jpg", Price =9.99},
                        new Icecream { Id = 8, Name = "Lemon", Image = "https://raw.githubusercontent.com/Abhayprince/Images-Icons/main/Icecreams/small/ic_7.jpg", Price =3.45},
                        new Icecream { Id = 9, Name = "Coffee", Image = "https://raw.githubusercontent.com/Abhayprince/Images-Icons/main/Icecreams/small/ic_8.jpg", Price =7.50},
                        new Icecream { Id = 10, Name = "Coconut", Image = "https://raw.githubusercontent.com/Abhayprince/Images-Icons/main/Icecreams/small/ic_9.jpg", Price =3.69},
                        new Icecream { Id = 11, Name = "Mango", Image = "https://raw.githubusercontent.com/Abhayprince/Images-Icons/main/Icecreams/small/ic_10.jpg", Price =1.09},
                        new Icecream { Id = 12, Name = "Blueberry", Image = "https://raw.githubusercontent.com/Abhayprince/Images-Icons/main/Icecreams/small/ic_11.jpg", Price =14.99},
                        new Icecream { Id = 13, Name = "Raspberry", Image = "https://raw.githubusercontent.com/Abhayprince/Images-Icons/main/Icecreams/small/ic_12.jpg", Price =5},
                        new Icecream { Id = 14, Name = "Butter Pecan", Image = "https://raw.githubusercontent.com/Abhayprince/Images-Icons/main/Icecreams/small/ic_13.jpg", Price =6.59},
                        new Icecream { Id = 15, Name = "Peach", Image = "https://raw.githubusercontent.com/Abhayprince/Images-Icons/main/Icecreams/small/ic_14.jpg", Price =1.99},
                        new Icecream { Id = 16, Name = "Cookies and Cream", Image = "https://raw.githubusercontent.com/Abhayprince/Images-Icons/main/Icecreams/small/ic_15.jpg", Price=3.34 },
                        new Icecream { Id = 17, Name = "Rocky Road", Image = "https://raw.githubusercontent.com/Abhayprince/Images-Icons/main/Icecreams/small/ic_16.jpg", Price =5.42},
                        new Icecream { Id = 18, Name = "Cherry", Image = "https://raw.githubusercontent.com/Abhayprince/Images-Icons/main/Icecreams/small/ic_17.jpg", Price =2.49},
                        new Icecream { Id = 19, Name = "Blackberry", Image = "https://raw.githubusercontent.com/Abhayprince/Images-Icons/main/Icecreams/small/ic_18.jpg" , Price=3.99},
                        new Icecream { Id = 20, Name = "Tiramisu", Image = "https://raw.githubusercontent.com/Abhayprince/Images-Icons/main/Icecreams/small/ic_19.jpg", Price=4.99 }
  ];
            IcecreamOptions[] icecreamOptions = [
                new IcecreamOptions { IcecreamId = 1, Flavor = "Vanilla", Topping = "Chocolate Chips" },
                new IcecreamOptions { IcecreamId = 1, Flavor = "Vanilla", Topping = "Caramel Drizzle" },
                new IcecreamOptions { IcecreamId = 2, Flavor = "Chocolate", Topping = "Whipped Cream" },
                new IcecreamOptions { IcecreamId = 3, Flavor = "Strawberry", Topping = "Sprinkles" },
                new IcecreamOptions { IcecreamId = 3, Flavor = "Strawberry", Topping = "White Chocolate" },
                new IcecreamOptions { IcecreamId = 4, Flavor = "Mint", Topping = "Oreo Crumbles" },
                new IcecreamOptions { IcecreamId = 4, Flavor = "Mint", Topping = "Chocolate Sauce" },
                new IcecreamOptions { IcecreamId = 5, Flavor = "Caramel", Topping = "Pecans" },
                new IcecreamOptions { IcecreamId = 5, Flavor = "Caramel", Topping = "Sea Salt" },
                new IcecreamOptions { IcecreamId = 6, Flavor = "Pistachio", Topping = "Almonds" },
                new IcecreamOptions { IcecreamId = 7, Flavor = "Cookie Dough", Topping = "Caramel Drizzle" },
                new IcecreamOptions { IcecreamId = 7, Flavor = "Cookie Dough", Topping = "Chocolate Chips" },
                new IcecreamOptions { IcecreamId = 8, Flavor = "Lemon", Topping = "Coconut Flakes" },
                new IcecreamOptions { IcecreamId = 9, Flavor = "Coffee", Topping = "Hazelnuts" },
                new IcecreamOptions { IcecreamId = 9, Flavor = "Coffee", Topping = "Cinnamon" },
                new IcecreamOptions { IcecreamId = 10, Flavor = "Coconut", Topping = "Pineapple" },
                new IcecreamOptions { IcecreamId = 11, Flavor = "Mango", Topping = "Passionfruit Sauce" },
                new IcecreamOptions { IcecreamId = 12, Flavor = "Blueberry", Topping = "Granola" },
                new IcecreamOptions { IcecreamId = 12, Flavor = "Blueberry", Topping = "White Chocolate" },
                new IcecreamOptions { IcecreamId = 13, Flavor = "Raspberry", Topping = "White Chocolate" },
                new IcecreamOptions { IcecreamId = 13, Flavor = "Raspberry", Topping = "Dark Chocolate" },
                new IcecreamOptions { IcecreamId = 14, Flavor = "Butter Pecan", Topping = "Maple Syrup" },
                new IcecreamOptions { IcecreamId = 14, Flavor = "Butter Pecan", Topping = "Crushed Nuts" },
                new IcecreamOptions { IcecreamId = 15, Flavor = "Peach", Topping = "Crushed Graham Crackers" },
                new IcecreamOptions { IcecreamId = 16, Flavor = "Cookies and Cream", Topping = "Chocolate Sauce" },
                new IcecreamOptions { IcecreamId = 16, Flavor = "Cookies and Cream", Topping = "Caramel Sauce" },
                new IcecreamOptions { IcecreamId = 17, Flavor = "Rocky Road", Topping = "Mini Marshmallows" },
                new IcecreamOptions { IcecreamId = 18, Flavor = "Cherry", Topping = "Crushed Almonds" },
                new IcecreamOptions { IcecreamId = 19, Flavor = "Blackberry", Topping = "Walnuts" },
                new IcecreamOptions { IcecreamId = 20, Flavor = "Tiramisu", Topping = "Cocoa Powder" },
                new IcecreamOptions { IcecreamId = 20, Flavor = "Tiramisu", Topping = "Espresso Drizzle" }
            ];
            modelBuilder.Entity<Icecream>().HasData(icecreams);
            modelBuilder.Entity<IcecreamOptions>().HasData(icecreamOptions);

        }
    }
}