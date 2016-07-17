namespace API.Data.Repository
{
    using Core.Entities;
    using Interfaces;
    using Sql;
    using Sql.Interfaces;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Threading.Tasks;
    using System;

    public class CustomerRepository : ICustomerRepository
    {
        internal IApiContext _context;

        public CustomerRepository() : this(new ApiContext())
        {

        }

        public CustomerRepository(IApiContext context)
        {
            _context = context;
        }

        public async Task<IList<Customer>> GetAllAsync()
        {
            return await _context.Customers.ToListAsync();
        }

        public async Task<Customer> GetByIdAsync(string id)
        {
            return await _context.Customers.Include(x => x.Orders).FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
