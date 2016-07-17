namespace API.Data.Sql
{
    using Interfaces;
    using System.Data.Entity;
    using Core.Entities;
    using System;

    public class ApiContext : DbContext, IApiContext
    {
        public IDbSet<Customer> Customers { get; set; }
        public IDbSet<Order> Orders { get; set; }

        public ApiContext()
            : base("TechDatabase")
        {
        }
    }
}
