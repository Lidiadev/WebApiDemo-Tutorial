namespace API.Unit.Tests.Services
{
    using API.Services.Customer;
    using API.Services.Interfaces;
    using Core.Dtos.Customer;
    using Core.Entities;
    using Data.Repository.Interfaces;
    using NUnit.Framework;
    using Rhino.Mocks;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Testing.Core.Helpers;

    [TestFixture]
    public class CustomerReadServiceTests
    {
        private ICustomerRepository _customerRepository;
        private ICustomerReadService _subject;

        [SetUp]
        public void Setup()
        {
            _customerRepository = MockRepository.GenerateMock<ICustomerRepository>();
            _subject = new CustomerReadService(_customerRepository);
        }

        [Test]
        public async Task GetAllAsync_CustomersFound_ReturnsCustomers()
        {
            // ARRANGE
            IList<Customer> customers = TestHelpers.CreateCustomers();
            _customerRepository.Stub(x => x.GetAllAsync()).Return(Task.FromResult(customers));

            // ACT
            IList<CustomerDto> result = await _subject.GetAllAsync();

            // ASSERT
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Any(), Is.True);

            _customerRepository.AssertWasCalled(x => x.GetAllAsync());
        }

        [Test]
        public async Task GetAllAsync_CustomersNotFound_ReturnsEmptyList()
        {
            // ARRANGE
            IList<Customer> customers = new List<Customer>();
            _customerRepository.Stub(x => x.GetAllAsync()).Return(Task.FromResult(customers));

            // ACT
            IList<CustomerDto> result = await _subject.GetAllAsync();

            // ASSERT
            Assert.That(result.Any(), Is.False);

            _customerRepository.AssertWasCalled(x => x.GetAllAsync());
        }

    }
}
