using AutoMapper;
using Store.Data.Entities;
using Store.Data.Entities.IDentityEntities;
using Store.Data.Entities.OrderEntities;
using Store.Repository.Interfaces;
using Store.Repository.Specification.OrderSpecifics;
using Store.Service.Services.BasketServices;
using Store.Service.Services.OrderServices.Dtos;
using Store.Service.Services.PaymentService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Service.Services.OrderServices
{
    public class OrderServices : IOrderServices
    {
        private readonly IBasketServices _basketServices;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IPaymentService _paymentService;

        public OrderServices(IBasketServices basketServices, IUnitOfWork unitOfWork, IMapper mapper, IPaymentService paymentService)
        {
            _basketServices = basketServices;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _paymentService = paymentService;
        }
        public async Task<OrderDetailsDto> CreateOrderAsync(OrderDto input)
        {
            var basket = await _basketServices.GetBasketAsync(input.BasketId);
            if (basket is null)
                throw new Exception("Basket New Exist");
            var orderItems = new List<OrderItemDto>();

            foreach (var basketItem in basket.basketItems)
            {
                var productItems = await _unitOfWork.Repository<Products, int>().GetByIdAsync(basketItem.ProductId);
                if (productItems is null)
                    throw new Exception($"Product With Id :{basketItem.ProductId} not Exist");

                var itemOrdered = new ProductItem
                {
                    ProductID = productItems.Id,
                    ProductName = productItems.Name,
                    PictureUrl = productItems.PictureUrl
                };
                var orderItem = new OrderItem
                {
                    Price = productItems.Price,
                    Quality = basketItem.Quantity,
                    ProductItem = itemOrdered
                };
                var mappedItem = _mapper.Map<OrderItemDto>(orderItem);
                orderItems.Add(mappedItem);
            }

            var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod, int>().GetByIdAsync(input.DeliveryMethodId);

            if (deliveryMethod is null)
                throw new Exception("Delivery Method Not Providers");

            var subtotal = orderItems.Sum(item => item.Quantity * item.Price);

            var mappedshippingAddress = _mapper.Map<ShippingAddress>(input.shippingAddress);
            var mappedOrderItem = _mapper.Map<List<OrderItem>>(orderItems);
            var order = new Order
            {
                deliveryMethodId = deliveryMethod.Id,
                shippingAddress = mappedshippingAddress,
                BuyerEmail = input.BasketEmail,
                BasketId = input.BasketId,
                orderItems = mappedOrderItem,
                SubTotal = subtotal 
            };
            await _unitOfWork.Repository<Order, Guid>().AddAsync(order);
            await _unitOfWork.CompleteAsync();

            var mappedOrder = _mapper.Map<OrderDetailsDto>(order);
            return mappedOrder;
        }

        public async Task<IReadOnlyList<DeliveryMethod>> GetAllDeliveryMethodsAsync()
            => await _unitOfWork.Repository<DeliveryMethod, int>().GetAllAsync();

        public async Task<IReadOnlyList<OrderDetailsDto>> GetAllForUserAsync(string BuyerEmail)
        {
            var specs = new OrderWithItemSpecifications(BuyerEmail);
            var orders = await _unitOfWork.Repository<Order, Guid>().GetAllWithSpecificationAsync(specs);
            if (!orders.Any())
                throw new Exception("You Do not Have any order yet");
            var mappedOrders = _mapper.Map<List<OrderDetailsDto>>(orders);
            return mappedOrders;
        }

        public async Task<OrderDetailsDto> GetOrderByIdAsync(Guid Id)
        {
            var specs = new OrderWithItemSpecifications(Id);
            var orders = await _unitOfWork.Repository<Order, Guid>().GetWithSpecificationByIdAsync(specs);

            if (orders is null)
                throw new Exception($"there is no order with id {Id}");
            var mappedOrders = _mapper.Map<OrderDetailsDto>(orders);
            return mappedOrders;
        }
    }
}
