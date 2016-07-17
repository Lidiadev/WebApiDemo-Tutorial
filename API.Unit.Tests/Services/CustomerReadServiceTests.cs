namespace API.Unit.Tests.Services
{
    using API.Services.Customer;
    using API.Services.Interfaces;
    using Core.Dtos.Customer;
    using Core.Entities;
    using Data.Repository.Interfaces;
    using Library.Common;
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

        [Test]
        public async Task GetByIdAsync_CustomerFoundWithOrders_ReturnsCustomer()
        {
            // ARRANGE
            Customer customer = TestHelpers.CreateCustomer();
            _customerRepository.Stub(x => x.GetByIdAsync(customer.Id)).Return(Task.FromResult(customer));

            // ACT
            CustomerDetailDto result = await _subject.GetByIdAsync(customer.Id);

            // ASSERT
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(result.Id));
            Assert.That(result.Email, Is.EqualTo(result.Email));
            Assert.That(result.Name, Is.EqualTo(result.Name));
            Assert.That(result.Orders.Any, Is.True);

            _customerRepository.AssertWasCalled(x => x.GetByIdAsync(customer.Id));
        }

        [Test]
        public async Task GetByIdAsync_CustomerFoundNoOrders_ReturnsCustomer()
        {
            // ARRANGE
            Customer customer = TestHelpers.CreateCustomerWithoutOrders();
            _customerRepository.Stub(x => x.GetByIdAsync(customer.Id)).Return(Task.FromResult(customer));

            // ACT
            CustomerDetailDto result = await _subject.GetByIdAsync(customer.Id);

            // ASSERT
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(result.Id));
            Assert.That(result.Email, Is.EqualTo(result.Email));
            Assert.That(result.Name, Is.EqualTo(result.Name));
            Assert.That(result.Orders.Any, Is.False);

            _customerRepository.AssertWasCalled(x => x.GetByIdAsync(customer.Id));
        }

        [Test]
        public async Task GetByIdAsync_CustomerNotFound_ReturnsNull()
        {
            // ARRANGE
            string customerId = CommonFunctions.GenerateId();
            _customerRepository.Stub(x => x.GetByIdAsync(customerId)).Return(Task.FromResult(null as Customer));

            // ACT
            CustomerDetailDto result = await _subject.GetByIdAsync(customerId);

            // ASSERT
            Assert.That(result, Is.Null);

            _customerRepository.AssertWasCalled(x => x.GetByIdAsync(customerId));
        }

    }
}
