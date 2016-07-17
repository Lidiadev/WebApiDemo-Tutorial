namespace API.Core.Dtos.Order
{
    using Entities;
    using System;

    public class OrderDetailDto
    {
        public string Id { get; set; }
        public double Price { get; set; }
        public DateTime CreatedDate { get; set; }

        public OrderDetailDto()
        {

        }

        public OrderDetailDto(Order orderEntity)
        {
            Id = orderEntity.Id;
            Price = orderEntity.Price;
            CreatedDate = orderEntity.CreatedDate;
        }
    }
}
