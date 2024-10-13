using Store.Data.Entities;
using Store.Service.Services.OrderServices.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Service.Services.OrderServices
{
    public interface IOrderServices
    {
        Task<OrderDetailsDto> CreateOrderAsync(OrderDto input);
        Task<IReadOnlyList<OrderDetailsDto>> GetAllForUserAsync(string BuyerEmail);
        Task<OrderDetailsDto> GetOrderByIdAsync(Guid Id);
        Task<IReadOnlyList<DeliveryMethod>> GetAllDeliveryMethodsAsync();
    }
}
