namespace API.Testing.Core.Helpers
{
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
    }
}
