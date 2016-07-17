namespace API.Core.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Order
    {
        public string Id { get; set; }

        [Required()]
        public double Price { get; set; }

        public DateTime CreatedDate { get; set; }

        public string CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
