using Store.Data.Entities.IDentityEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Store.Data.Entities;
namespace Store.Data.Entities.OrderEntities
{
    public class Order : BaseEntity<Guid>
    {
        public string BuyerEmail { get; set; }
        public DateTimeOffset orderDate { get; set; } = DateTimeOffset.Now;
        public ShippingAddress shippingAddress { get; set; }
        public DeliveryMethod deliveryMethod { get; set; }
        public int? deliveryMethodId { get; set; }
        public OrderStatus status { get; set; } = OrderStatus.Placed;
        public OverPaymentStatus overPaymentStatus { get; set; } = OverPaymentStatus.Pending;
        public IReadOnlyList<OrderItem> orderItems { get; set; }
        public decimal SubTotal { get; set; }
        public decimal GetTotal()
            => SubTotal + deliveryMethod.Price;
        public string? BasketId { get; set; }
    }
}
