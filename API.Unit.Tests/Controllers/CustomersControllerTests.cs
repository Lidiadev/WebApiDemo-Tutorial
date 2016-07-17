namespace API.Unit.Tests.Controllers
{
    using Api.Controllers;
    using Api.Models.Customer;
    using API.Services.Interfaces;
    using Core.Dtos.Customer;
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
    }
}
