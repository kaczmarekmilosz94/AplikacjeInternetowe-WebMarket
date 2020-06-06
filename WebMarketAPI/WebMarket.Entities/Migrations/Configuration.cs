using System.Data.Entity.Migrations;
using System.Linq;
using WebMarket.Entities.Models;

namespace WebMarket.Entities.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<WebMarket.Entities.AppDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(WebMarket.Entities.AppDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.


            if (!context.Categories.Any())
            {
                var category = new Category()
                {
                    Name = "Motoryzacja"
                };

                var user = new User()
                {
                    Username = "AdamNowak420",
                    Password = "adamnowak1",
                    Email = "adam420@gmail.com",
                    Adress = "Toruń ul. Długa 20a",
                    PhoneNumber = "666777888",
                    Basket = new Basket()
                };

                context.Categories.Add(category);
                context.Users.Add(user);

                context.SaveChanges();

                if (!context.Products.Any()) 
                {
                    var product1 = new Product()
                    {
                        CategoryId = category.Id,
                        Price = 100,
                        Name = "Samochód",
                        SellerId = user.Id
                    };

                    var product2 = new Product()
                    {
                        CategoryId = category.Id,
                        Price = 500,
                        Name = "Bus",
                        SellerId = user.Id
                    };

                    context.Products.Add(product1);
                    context.Products.Add(product2);
                    context.SaveChanges();
                }
            }
        }
    }
}
