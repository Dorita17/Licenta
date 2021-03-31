using System;
using System.Linq.Expressions;
using Core.Entities.OrderAggregate;

namespace Core.Specifications
{
    public class OrdersWithItemsAndOrderingSpecification : BaseSpecification<Order>
    {
        public OrdersWithItemsAndOrderingSpecification(string email)
            : base(o => o.BuyerEmail == email)
        {
            AddInclude(p => p.OrderItems);
            AddInclude(p => p.DeliveryMethod);
            AddOrderByDescending(p => p.OrderDate);
        }

        public OrdersWithItemsAndOrderingSpecification(int id, string email) 
            : base(o => o.Id == id && o.BuyerEmail == email)
        {
            AddInclude(p => p.OrderItems);
            AddInclude(p => p.DeliveryMethod);
            AddOrderByDescending(p => p.OrderDate);
        }
    }
}