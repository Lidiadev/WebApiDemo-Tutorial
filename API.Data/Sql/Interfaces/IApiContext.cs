namespace API.Data.Sql.Interfaces
{
    using Core.Entities;
    using System.Data.Entity;

    public interface IApiContext
    {
        IDbSet<Customer> Customers { get; set; }
    }
}
