namespace API.Services.Customer
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Core.Dtos.Customer;
    using Interfaces;
    using Data.Repository.Interfaces;
    using Data.Repository;
    using System.Linq;

    public class CustomerReadService : ICustomerReadService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerReadService()
            : this(new CustomerRepository())
        {
        }

        public CustomerReadService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<IList<CustomerDto>> GetAllAsync()
        {
            var customers = await _customerRepository.GetAllAsync();
            return customers.Any()
               ? customers.Select(x => new CustomerDto(x)).ToList()
               : new List<CustomerDto>();
        }
    }
}
