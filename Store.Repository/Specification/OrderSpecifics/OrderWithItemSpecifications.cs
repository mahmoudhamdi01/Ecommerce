using StackExchange.Redis;
using Store.Data.Entities.OrderEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Order = Store.Data.Entities.OrderEntities.Order;

namespace Store.Repository.Specification.OrderSpecifics
{
    public class OrderWithItemSpecifications : BaseSpecification<Order>
    {
        public OrderWithItemSpecifications(string buyerEmail)
            : base(order => order.BuyerEmail == buyerEmail)
        {
            AddInclude(o => o.deliveryMethod);
            AddInclude(o => o.orderItems);
            AddorderByDescending(o => o.orderDate);
        }
        public OrderWithItemSpecifications(Guid id)
            : base(order => order.Id == id)
        {
            AddInclude(o => o.deliveryMethod);
            AddInclude(o => o.orderItems);
        }
    }
}
