namespace API.Testing.Core.Helpers
{
    using API.Core.Dtos.Customer;
    using API.Core.Dtos.Order;
    using API.Core.Entities;
    using EntityFramework;
    using Ploeh.AutoFixture;
    using System.Collections.Generic;
    using System.Linq;

    public static class TestHelpers
    {
        private static readonly Fixture Fixture;

        static TestHelpers()
        {
            Fixture = new Fixture();
            Fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

        public static Customer CreateCustomer()
        {
            return Fixture.Build<Customer>()
                .Create();
        }

        public static Customer CreateCustomerWithoutOrders()
        {
            return Fixture.Build<Customer>()
                .Without(x => x.Orders)
                .Create();
        }

        public static CustomerDetailDto CreateCustomerDetailDto()
        {
            return Fixture.Build<CustomerDetailDto>()
                .Create();
        }

        public static CustomerDetailDto CreateCustomerDetailDtoWithoutOrders()
        {
            return Fixture.Build<CustomerDetailDto>()
                .With(x => x.Orders, new List<OrderDetailDto>())
                .Create();
        }

        public static IList<Customer> CreateCustomers(int number = 10)
        {
            return Fixture.Build<Customer>()
               .Without(x => x.Orders)
               .CreateMany(number)
               .ToList();
        }

        public static FakeDbSet<Customer> CreateFakeCustomers(int number = 10)
        {
            FakeDbSet<Customer> customersSet = new FakeDbSet<Customer>();

            IList<Customer> customers = Fixture.Build<Customer>()
               .CreateMany(number)
               .ToList();

            foreach (Customer customer in customers)
            {
                customersSet.Add(customer);
            }

            return customersSet;
        }

        public static IList<CustomerDto> CreateCustomerDtos(int number = 10)
        {
            return Fixture.Build<CustomerDto>()
               .CreateMany(number)
               .ToList();
        }
    }
}
