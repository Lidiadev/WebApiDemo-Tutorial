namespace API.Services.Customer
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Core.Dtos.Customer;
    using Interfaces;
    using Data.Repository.Interfaces;
    using Data.Repository;
    using System.Linq;
    using System;

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

        public async Task<CustomerDetailDto> GetByIdAsync(string id)
        {
            var customer = await _customerRepository.GetByIdAsync(id);
            return customer != null ? new CustomerDetailDto(customer) : null;
        }
    }
}
