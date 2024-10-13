using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Service.Services.OrderServices.Dtos
{
    public class OrderDto
    {
        public string BasketId { get; set; }
        public string BasketEmail { get; set; }
        public int DeliveryMethodId { get; set; }
        public AddressDto shippingAddress { get; set; }
    }
}
