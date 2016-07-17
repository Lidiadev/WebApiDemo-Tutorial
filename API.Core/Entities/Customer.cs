namespace API.Core.Entities
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Customer
    {
        public string Id { get; set; }

        [Required()]
        public string Name { get; set; }

        [Required()]
        public string Email { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
