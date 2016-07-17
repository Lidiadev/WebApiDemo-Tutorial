namespace API.Data.Migrations
{
    using Core.Entities;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<API.Data.Sql.ApiContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(API.Data.Sql.ApiContext context)
        {
            //  This method will be called after migrating to the latest version.


            Customer customer1 = new Customer { Id = "06df40e6792843ed8a1958fe003f65bf", Name = "Andrew Peters", Email = "andrew@yahoo.com" };
            Customer customer2 = new Customer { Id = "26f8bd2ed0ee40d9902dbbb6bd5eb4f8", Name = "Brice Lambson", Email = "brice@yahoo.com" };
            Customer customer3 = new Customer { Id = "9c03822a2ca640ab8d94fb7d57f0ded0", Name = "Rowan Miller", Email = "rowan@yahoo.com" };

            context.Customers.AddOrUpdate(
              p => p.Id,
              customer1,
              customer2,
              customer3
            );

            context.Orders.AddOrUpdate(
              p => p.Id,
              new Order { Id = "a5b8d7a858724e2eb3dcb925710de1b9", Price = 2300, CreatedDate = DateTime.Now, CustomerId = customer1.Id },
              new Order { Id = "cd05c78078f241e689e118f5612c1f45", Price = 4000, CreatedDate = DateTime.Now.AddDays(-3), CustomerId = customer1.Id },
              new Order { Id = "d5f50be58a0a488cad26bfe02c39d3c9", Price = 800, CreatedDate = DateTime.Now.AddDays(-2), CustomerId = customer1.Id },
              new Order { Id = "8c37331e1d0e461fa1fa9c06df2168f3", Price = 8000, CreatedDate = DateTime.Now, CustomerId = customer1.Id },
              new Order { Id = "791e4697beb744d9a4eb71e591b174ae", Price = 3000, CreatedDate = DateTime.Now, CustomerId = customer2.Id }
            );
        }
    }
}
