namespace Api.Models.Order
{
    using API.Core.Dtos.Order;
    using System;

    public class OrderDetailModel
    {
        public string Id { get; set; }
        public double Price { get; set; }
        public DateTime CreatedDate { get; set; }

        public OrderDetailModel()
        {

        }

        public OrderDetailModel(OrderDetailDto orderDto)
        {
            Id = orderDto.Id;
            Price = orderDto.Price;
            CreatedDate = orderDto.CreatedDate;
        }
    }
}