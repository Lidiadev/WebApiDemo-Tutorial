namespace API.Services.Interfaces
{
    using Core.Dtos.Customer;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICustomerReadService
    {
        Task<IList<CustomerDto>> GetAllAsync();
        Task<CustomerDetailDto> GetByIdAsync(string id);
    }
}
