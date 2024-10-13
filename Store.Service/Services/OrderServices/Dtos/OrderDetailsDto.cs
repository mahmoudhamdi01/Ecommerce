using Store.Data.Entities.IDentityEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Service.Services.OrderServices.Dtos
{
    public class OrderDetailsDto
    {
        public Guid Id { get; set; }
        public string BuyerEmail { get; set; }
        public DateTimeOffset OrderDate { get; set; }
        public string DeliveryMethodName { get; set; }
        public AddressDto shippingAddress {  get; set; }
        public OverPaymentStatus OverPaymentStatus { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public IReadOnlyList<OrderItemDto> orderItems { get; set; }
        public decimal Suptotal { get; set; }
        public decimal shippingPrice { get; set; }
        public decimal Total { get; set; }
        public string? BasketId { get; set; }
        public string? PaymentIntenId { get; set; }
    }
}
