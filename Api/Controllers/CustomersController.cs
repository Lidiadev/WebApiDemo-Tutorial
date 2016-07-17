namespace Api.Controllers
{
    using API.Core.Dtos.Customer;
    using API.Services.Customer;
    using API.Services.Interfaces;
    using Models.Customer;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Web.Http;

    public class CustomersController : ApiController
    {
        private readonly ICustomerReadService _customerReadService;

        public CustomersController() : this(new CustomerReadService())
        {
        }

        public CustomersController(ICustomerReadService customerReadService)
        {
            _customerReadService = customerReadService;
        }

        // GET api/customer
        /// <summary>
        /// Get all customers, without orders
        /// </summary>
        /// <returns>list of customers</returns>
        [HttpGet]
        [Route("api/v1/Customers")]
        public async Task<HttpResponseMessage> Get()
        {
            IList<CustomerDto> customers = await _customerReadService.GetAllAsync();
            return Request.CreateResponse(HttpStatusCode.OK,
                customers.Any() ? customers.Select(x => new CustomerModel(x)) : Array.Empty<CustomerModel>());
        }
    }
}
