namespace API.Testing.Core.EntityFramework
{
    using API.Core.Entities;
    using Data.Sql.Interfaces;
    using System.Data.Entity;

    public class TestContext : DbContext, IApiContext
    {
        public IDbSet<Customer> Customers { get; set; }
        public IDbSet<Order> Orders { get; set; }
    }
}
