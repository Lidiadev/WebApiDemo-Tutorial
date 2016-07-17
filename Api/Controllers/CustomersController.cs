namespace Api.Controllers
{
    using API.Core.Dtos.Customer;
    using API.Services.Customer;
    using API.Services.Interfaces;
    using Library.Common;
    using Models.Customer;
    using Models.Error;
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

        // GET api/v1/customers
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

        // GET api/v1/Customers/06df40e6792843ed8a1958fe003f65bf
        /// <summary>
        /// Get a customer by id and his order list 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>customer details and his order list</returns>
        [HttpGet]
        [Route("api/v1/Customers/{id}")]
        public async Task<HttpResponseMessage> Get(string id)
        {
            CustomerDetailDto customer = await _customerReadService.GetByIdAsync(id);
            return customer != null
                ? Request.CreateResponse(HttpStatusCode.OK, new CustomerDetailModel(customer))
                : Request.CreateResponse(HttpStatusCode.NotFound, new ErrorEnvelope(ErrorResponseConstants.ERROR_CUSTOMER_NOT_FOUND, ErrorResponseConstants.ERROR_CUSTOMER_NOT_FOUND_DESCRIPTION));
        }
    }
}
