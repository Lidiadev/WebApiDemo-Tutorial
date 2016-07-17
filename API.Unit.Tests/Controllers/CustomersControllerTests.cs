namespace API.Unit.Tests.Controllers
{
    using Api.Controllers;
    using Api.Models.Customer;
    using Api.Models.Error;
    using API.Services.Interfaces;
    using Core.Dtos.Customer;
    using Library.Common;
    using NUnit.Framework;
    using Rhino.Mocks;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Web.Http.Routing;
    using Testing.Core.Helpers;

    [TestFixture]
    public class CustomersControllerTests
    {
        private ICustomerReadService _customerReadService;
        private CustomersController _subject;

        [SetUp]
        public void Setup()
        {
            _customerReadService = MockRepository.GenerateMock<ICustomerReadService>();
            _subject = new CustomersController(_customerReadService);
        }

        [Test]
        public async Task Get_GetAll_ReturnsCustomers()
        {
            // ARRANGE
            IList<CustomerDto> customers = TestHelpers.CreateCustomerDtos();
            _customerReadService.Stub(x => x.GetAllAsync()).Return(Task.FromResult(customers));
            _subject.MockRequest(HttpMethod.Get, new HttpRouteValueDictionary { { "controller", "Customer" } });

            // ACT
            HttpResponseMessage result = await _subject.Get();

            // ASSERT
            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            var customersResponse = await result.Content.ReadAsAsync<IEnumerable<CustomerModel>>();
            Assert.That(customersResponse.Count(), Is.EqualTo(customers.Count()));

            _customerReadService.AssertWasCalled(x => x.GetAllAsync());
        }

        [Test]
        public async Task Get_GetAllNoCustomerFound_ReturnsOK()
        {
            // ARRANGE
            IList<CustomerDto> customers = new List<CustomerDto>();
            _customerReadService.Stub(x => x.GetAllAsync()).Return(Task.FromResult(customers));
            _subject.MockRequest(HttpMethod.Get, new HttpRouteValueDictionary { { "controller", "Customer" } });

            // ACT
            HttpResponseMessage result = await _subject.Get();

            // ASSERT
            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            _customerReadService.AssertWasCalled(x => x.GetAllAsync());
        }

        [Test]
        public async Task Get_GetByIdCustomerWithOrders_ReturnsOK()
        {
            // ARRANGE
            CustomerDetailDto customer = TestHelpers.CreateCustomerDetailDto();
            _customerReadService.Stub(x => x.GetByIdAsync(customer.Id)).Return(Task.FromResult(customer));
            _subject.MockRequest(HttpMethod.Get, new HttpRouteValueDictionary { { "controller", "Customer" } });

            // ACT
            HttpResponseMessage result = await _subject.Get(customer.Id);

            // ASSERT
            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            CustomerDetailModel customerResponse = await result.Content.ReadAsAsync<CustomerDetailModel>();
            Assert.That(customerResponse, Is.Not.Null);
            Assert.That(customerResponse.Id, Is.EqualTo(customer.Id));
            Assert.That(customerResponse.Name, Is.EqualTo(customer.Name));
            Assert.That(customerResponse.Email, Is.EqualTo(customer.Email));
            Assert.That(customerResponse.Orders.Any(), Is.True);

            _customerReadService.AssertWasCalled(x => x.GetByIdAsync(customer.Id));
        }

        [Test]
        public async Task Get_GetByIdCustomerWithNoOrders_ReturnsOK()
        {
            // ARRANGE
            CustomerDetailDto customer = TestHelpers.CreateCustomerDetailDtoWithoutOrders();
            _customerReadService.Stub(x => x.GetByIdAsync(customer.Id)).Return(Task.FromResult(customer));
            _subject.MockRequest(HttpMethod.Get, new HttpRouteValueDictionary { { "controller", "Customer" } });

            // ACT
            HttpResponseMessage result = await _subject.Get(customer.Id);

            // ASSERT
            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            CustomerDetailModel customerResponse = await result.Content.ReadAsAsync<CustomerDetailModel>();
            Assert.That(customerResponse, Is.Not.Null);
            Assert.That(customerResponse.Id, Is.EqualTo(customer.Id));
            Assert.That(customerResponse.Name, Is.EqualTo(customer.Name));
            Assert.That(customerResponse.Email, Is.EqualTo(customer.Email));
            Assert.That(customerResponse.Orders.Any(), Is.False);

            _customerReadService.AssertWasCalled(x => x.GetByIdAsync(customer.Id));
        }

        [Test]
        public async Task Get_GetById_ReturnsNotFound()
        {
            // ARRANGE
            string customerId = CommonFunctions.GenerateId();
            _customerReadService.Stub(x => x.GetByIdAsync(customerId)).Return(Task.FromResult(null as CustomerDetailDto));
            _subject.MockRequest(HttpMethod.Get, new HttpRouteValueDictionary { { "controller", "Customer" } });

            // ACT
            HttpResponseMessage result = await _subject.Get(customerId);

            // ASSERT
            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));

            ErrorEnvelope envelope = await result.Content.ReadAsAsync<ErrorEnvelope>();
            Assert.That(envelope.Error.Code, Is.EqualTo(ErrorResponseConstants.ERROR_CUSTOMER_NOT_FOUND));
            Assert.That(envelope.Error.Message, Is.EqualTo(ErrorResponseConstants.ERROR_CUSTOMER_NOT_FOUND_DESCRIPTION));

            _customerReadService.AssertWasCalled(x => x.GetByIdAsync(customerId));
        }

    }
}
