namespace API.Data.Repository.Interfaces
{
    using Core.Entities;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICustomerRepository
    {
        Task<IList<Customer>> GetAllAsync();
        Task<Customer> GetByIdAsync(string id);
    }
}
