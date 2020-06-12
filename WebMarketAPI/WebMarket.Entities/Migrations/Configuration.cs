namespace WebMarket.Entities.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using WebMarket.Entities.Identity;
    using WebMarket.Entities.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<WebMarket.Entities.AppDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(WebMarket.Entities.AppDbContext context)
        {
            if (!context.Users.Any())
            {
                var passwordHasher = new PasswordHasher();

                var role1 = new IdentityRole("Admin");
                var role2 = new IdentityRole("User");
                context.Roles.Add(role1);
                context.Roles.Add(role2);

                var user1 = new User("admin")
                {
                    PasswordHash = passwordHasher.HashPassword("Admin123!"),
                    SecurityStamp = Guid.NewGuid().ToString(),
                    Email = "admin@webmarket.pl",
                    PhoneNumber = "666666666",
                    Address = "ul. Szkolna 5a, Toruń",
                    Basket = new Models.Basket()
                };
                var user2 = new User("tomek12")
                {
                    PasswordHash = passwordHasher.HashPassword("Tomek123!"),
                    SecurityStamp = Guid.NewGuid().ToString(),
                    Email = "tomek12@onet.pl",
                    PhoneNumber = "777777777",
                    Address = "ul. Długa 23/24, Gdańsk",
                    Basket = new Models.Basket()
                };
                var user3 = new User("adam15")
                {
                    PasswordHash = passwordHasher.HashPassword("Adam123!"),
                    SecurityStamp = Guid.NewGuid().ToString(),
                    Email = "adam15@wp.pl",
                    PhoneNumber = "888888888",
                    Address = "ul. Ogólna 15/2, Warszawa",
                    Basket = new Models.Basket()
                };

                var userRole1 = new IdentityUserRole
                {
                    RoleId = role1.Id,
                    UserId = user1.Id
                };
                var userRole2 = new IdentityUserRole
                {
                    RoleId = role2.Id,
                    UserId = user2.Id
                };
                var userRole3 = new IdentityUserRole
                {
                    RoleId = role2.Id,
                    UserId = user3.Id
                };

                user1.Roles.Add(userRole1);
                user2.Roles.Add(userRole2);
                user3.Roles.Add(userRole3);

                context.Users.Add(user1);
                context.Users.Add(user2);
                context.Users.Add(user3);

                context.SaveChanges();

                var cat1 = new Category()
                {
                    Name = "Motoryzacja"
                };
                var cat2 = new Category()
                {
                    Name = "Elektronika"
                };
                var cat3 = new Category()
                {
                    Name = "Sport"
                };

                context.Categories.Add(cat1);
                context.Categories.Add(cat2);
                context.Categories.Add(cat3);

                context.SaveChanges();

                // Motoryzacja
                var prod1 = new Product()
                {
                    Name = "Mercedes-Benz Klasa E",
                    CategoryId = cat1.Id,
                    SellerId = user3.Id,
                    Price = 43900,
                    ImageURL = "moto1.png"
                };
                var prod2 = new Product()
                {
                    Name = "Citroen C5",
                    CategoryId = cat1.Id,
                    SellerId = user3.Id,
                    Price = 30900,
                    ImageURL = "moto2.png"
                };

                // Elektronika
                var prod3 = new Product()
                {
                    Name = "PlayStation 2 Fat",
                    CategoryId = cat2.Id,
                    SellerId = user2.Id,
                    Price = 299,
                    ImageURL = "elek1.png"
                };
                var prod4 = new Product()
                {
                    Name = "Nintendo 3DS XL + Gry",
                    CategoryId = cat2.Id,
                    SellerId = user2.Id,
                    Price = 999,
                    ImageURL = "elek2.png"
                };

                // Sport
                var prod5 = new Product()
                {
                    Name = "Narty Rossignol ",
                    CategoryId = cat3.Id,
                    SellerId = user2.Id,
                    Price = 575,
                    ImageURL = "sport1.png"
                };
                var prod6 = new Product()
                {
                    Name = "ROWER GÓRSKI MTB 27,5",
                    CategoryId = cat3.Id,
                    SellerId = user3.Id,
                    Price = 969,
                    ImageURL = "sport2.png"
                };

                context.Products.Add(prod1);
                context.Products.Add(prod2);
                context.Products.Add(prod3);
                context.Products.Add(prod4);
                context.Products.Add(prod5);
                context.Products.Add(prod6);

                context.SaveChanges();
            }
        }
    }
}
