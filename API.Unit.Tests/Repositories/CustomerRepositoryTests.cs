namespace API.Unit.Tests.Repositories
{
    using Core.Entities;
    using Data.Repository;
    using Data.Repository.Interfaces;
    using Library.Common;
    using NUnit.Framework;
    using Rhino.Mocks;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Testing.Core.EntityFramework;
    using Testing.Core.Helpers;
    using TestContext = Testing.Core.EntityFramework.TestContext;

    [TestFixture]
    public class CustomerRepositoryTests
    {
        private TestContext _context;
        private ICustomerRepository _subject;
        private FakeDbSet<Customer> _customersDbSet;

        [SetUp]
        public void Setup()
        {
            _customersDbSet = TestHelpers.CreateFakeCustomers();
            _context = MockRepository.GenerateMock<TestContext>();

            _subject = new CustomerRepository(_context);
        }

        [Test]
        public async Task GetAllAsync_CustomersFound_ReturnsCustomers()
        {
            // ARRANGE
            _context.Customers = _customersDbSet;

            // ACT
            IList<Customer> result = await _subject.GetAllAsync();

            // ASSERT
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(_customersDbSet.Count()));

            _context.VerifyAllExpectations();
        }

        [Test]
        public async Task GetAllAsync_CustomersNotFound_ReturnsEmptyList()
        {
            // ARRANGE
            _context.Customers = new FakeDbSet<Customer>();

            // ACT
            IList<Customer> result = await _subject.GetAllAsync();

            // ASSERT
            Assert.That(result, Is.Empty);

            _context.VerifyAllExpectations();
        }

        [Test]
        public async Task GetByIdAsync_CustomerFound_ReturnsCustomer()
        {
            // ARRANGE
            _context.Customers = _customersDbSet;
            string customerId = _customersDbSet.First().Id;

            // ACT
            Customer result = await _subject.GetByIdAsync(customerId);

            // ASSERT
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(customerId));

            _context.VerifyAllExpectations();
        }

        [Test]
        public async Task GetByIdAsync_CustomerNotFound_ReturnsNull()
        {
            // ARRANGE
            _context.Customers = _customersDbSet;

            // ACT
            Customer result = await _subject.GetByIdAsync(CommonFunctions.GenerateId());

            // ASSERT
            Assert.That(result, Is.Null);

            _context.VerifyAllExpectations();
        }
    }
}
