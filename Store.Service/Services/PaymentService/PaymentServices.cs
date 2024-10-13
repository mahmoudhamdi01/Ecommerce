using AutoMapper;
using Microsoft.Extensions.Configuration;
using Store.Data.Entities;
using Store.Data.Entities.OrderEntities;
using Store.Repository.Interfaces;
using Store.Repository.Specification.OrderSpecifics;
using Store.Service.Services.BasketServices;
using Store.Service.Services.BasketServices.Dto;
using Store.Service.Services.OrderServices.Dtos;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Service.Services.PaymentService
{
    public class PaymentServices : IPaymentService
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBasketServices _basketServices;

        public PaymentServices(IConfiguration configuration,IMapper mapper, IUnitOfWork unitOfWork, IBasketServices basketServices)
        {
            _configuration = configuration;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _basketServices = basketServices;
        }
        public async Task<OrderDetailsDto> CreateOrUpdatePaymentFailed(string paymentIntentId)
        {
            var specs = new OrderWithPaymentIntentSpecifications(paymentIntentId);
            var order = await _unitOfWork.Repository<Order, Guid>().GetWithSpecificationByIdAsync(specs);

            if (order is null)
                throw new Exception("order does not exist");

            _unitOfWork.Repository<Order, Guid>().Update(order);
            await _unitOfWork.CompleteAsync();

            var mappedOrder = _mapper.Map<OrderDetailsDto>(order);
            return mappedOrder;
        }

        public async Task<CustomerBasketDto> CreateOrUpdatePaymentIntent(CustomerBasketDto input)
        {
            StripeConfiguration.ApiKey = _configuration["Stripe:Secretkey"];
            if (input is null)
                throw new Exception("Basket is Empty");

            var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod, int>().GetByIdAsync(input.DeliveryMethodId.Value);

            if (deliveryMethod is null)
                throw new Exception("Delivery Method Not Providers");
            decimal shippingPrice = deliveryMethod.Price;
            foreach (var item in input.basketItems)
            {
                var product = await _unitOfWork.Repository<Products, int>().GetByIdAsync(item.ProductId);
                if(item.Price != product.Price)
                    item.Price = product.Price;
            }
            var service = new PaymentIntentCreateOptions();
            PaymentIntent paymentIntent;
            if(string.IsNullOrEmpty(input.PaymentIntenId))
            {
                var options = new PaymentIntentCreateOptions
                {
                    Amount = (long)input.basketItems.Sum(i => i.Quantity *( i.Price * 100)) + (long)(shippingPrice * 100),
                    Currency = "usd",
                    PaymentMethodTypes = new List<string> { "card" }
                };
                paymentIntent = await service.CreateAsync(options);
                input.PaymentIntenId = paymentIntent.Id;
                input.ClientSecret = paymentIntent.ClientSecret;
            }
            else
            {
                var options = new PaymentIntentUpdateOptions
                {
                    Amount = (long)input.basketItems.Sum(i => i.Quantity * (i.Price * 100)) + (long)(shippingPrice * 100),
                };

                await service.UpdateAsync(input.PaymentIntenId, options);
            }
            await _basketServices.UpdateBasketAsync(input);
            return input;
        }

        public async Task<OrderDetailsDto> CreateOrUpdatePaymentSucceeded(string paymentIntentId)
        {
            var specs = new OrderWithPaymentIntentSpecifications(paymentIntentId);
            var order = await _unitOfWork.Repository<Order, Guid>().GetWithSpecificationByIdAsync(specs);

            if (order is null)
                throw new Exception("order does not exist");
            _unitOfWork.Repository<Order, Guid>().Update(order);
            await _unitOfWork.CompleteAsync();

            await _basketServices.DeleteBasketAsync(order.BasketId);

            var mappedOrder = _mapper.Map<OrderDetailsDto>(order);
            return mappedOrder;
        }
    }
}
