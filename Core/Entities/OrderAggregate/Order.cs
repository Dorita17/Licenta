using System;
using System.Collections.Generic;

namespace Core.Entities.OrderAggregate
{
    public class Order : BaseEntity
    {
        public Order()
        {
        }

        public Order(string buyerEmail, decimal subtotal, Address deliverToAddress, DeliveryMethod deliveryMethod,
        IReadOnlyList<OrderItem> orderItems, string paymentIntentId)
        {
            BuyerEmail = buyerEmail;
            Subtotal = subtotal;
            DeliverToAddress = deliverToAddress;
            DeliveryMethod = deliveryMethod;
            OrderItems = orderItems;
            PaymentIntentId = paymentIntentId;
        }

        public string BuyerEmail { get; set; }
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;
        public decimal Subtotal { get; set; }
        public OrderStatus Status { get; set; } = OrderStatus.Pending;

        //Relationships
        public Address DeliverToAddress { get; set; }
        public DeliveryMethod DeliveryMethod { get; set; }
        public IReadOnlyList<OrderItem> OrderItems { get; set; }
        public string PaymentIntentId { get; set; }

        public decimal GetTotal()
        {
            return Subtotal + DeliveryMethod.Price;
        }

    }
}