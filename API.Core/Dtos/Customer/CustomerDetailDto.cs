namespace API.Core.Dtos.Customer
{
    using Entities;
    using Order;
    using System.Collections.Generic;
    using System.Linq;

    public class CustomerDetailDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public IList<OrderDetailDto> Orders { get; set; }

        public CustomerDetailDto()
        {

        }

        public CustomerDetailDto(Customer customerEntity)
        {
            Id = customerEntity.Id;
            Name = customerEntity.Name;
            Email = customerEntity.Email;
            Orders = customerEntity.Orders != null && customerEntity.Orders.Any()
              ? customerEntity.Orders.Select(x => new OrderDetailDto(x)).ToList()
              : new List<OrderDetailDto>();
        }
    }
}
