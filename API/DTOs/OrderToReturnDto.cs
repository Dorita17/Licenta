using System;
using System.Collections.Generic;
using Core.Entities.OrderAggregate;

namespace API.DTOs
{
    public class OrderToReturnDto
    {
        public int Id { get; set; }
        public string BuyerEmail { get; set; }
        public DateTimeOffset OrderDate { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Total { get; set; }
        public string DeliveryMethod { get; set; }
        public decimal DeliveryPrice { get; set; }
        public string Status { get; set; }
        public Address DeliverToAddress { get; set; }
        public IReadOnlyList<OrderItemDto> OrderItems { get; set; }
    }
}