using Store.Service.Services.BasketServices.Dto;
using Store.Service.Services.OrderServices.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Service.Services.PaymentService
{
    public interface IPaymentService
    {
        Task<CustomerBasketDto> CreateOrUpdatePaymentIntent(CustomerBasketDto input);
        Task<OrderDetailsDto> CreateOrUpdatePaymentSucceeded(string paymentIntentId);
        Task<OrderDetailsDto> CreateOrUpdatePaymentFailed(string paymentIntentId);
    }
}
