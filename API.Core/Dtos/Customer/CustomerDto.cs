namespace API.Core.Dtos.Customer
{
    using Entities;

    public class CustomerDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public CustomerDto()
        {

        }

        public CustomerDto(Customer customerEntity)
        {
            Id = customerEntity.Id;
            Name = customerEntity.Name;
            Email = customerEntity.Email;
        }
    }
}
