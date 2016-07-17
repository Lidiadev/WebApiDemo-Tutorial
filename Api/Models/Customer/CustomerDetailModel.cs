namespace Api.Models.Customer
{
    using API.Core.Dtos.Customer;
    using Order;
    using System.Collections.Generic;
    using System.Linq;

    public class CustomerDetailModel
    {

        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public IList<OrderDetailModel> Orders { get; set; }

        public CustomerDetailModel()
        {

        }

        public CustomerDetailModel(CustomerDetailDto customerDto)
        {
            Id = customerDto.Id;
            Name = customerDto.Name;
            Email = customerDto.Email;
            Orders = customerDto.Orders.Any()
               ? customerDto.Orders.Select(x => new OrderDetailModel(x)).ToList()
               : new List<OrderDetailModel>();
        }
    }
}