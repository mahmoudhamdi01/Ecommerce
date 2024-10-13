using AutoMapper;
using Store.Data.Entities.IDentityEntities;
using Store.Data.Entities.OrderEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Service.Services.OrderServices.Dtos
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<ShippingAddress, AddressDto>().ReverseMap();

            CreateMap<Order, OrderDetailsDto>()
            .ForMember(dest => dest.DeliveryMethodName, options => options.MapFrom(src => src.deliveryMethod.ShortName))
                .ForMember(dest => dest.shippingPrice, options => options.MapFrom(src => src.deliveryMethod.Price));

            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(dest => dest.ProductItemId, options => options.MapFrom(src => src.ItemOrder.ProductID))
                    .ForMember(dest => dest.ProductName, options => options.MapFrom(src => src.ItemOrder.ProductName))
                    .ForMember(dest => dest.PictureUrl, options => options.MapFrom<OrderItemPictureUrlresolver>()).ReverseMap();
        }
    }
}
