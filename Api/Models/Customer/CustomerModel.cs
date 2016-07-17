namespace Api.Models.Customer
{
    using API.Core.Dtos.Customer;

    public class CustomerModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public CustomerModel()
        {

        }

        public CustomerModel(CustomerDto customerDto)
        {
            Id = customerDto.Id;
            Name = customerDto.Name;
            Email = customerDto.Email;
        }
    }
}