using AutoMapper;
using Microsoft.Extensions.Configuration;
using Store.Data.Entities.IDentityEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Service.Services.OrderServices.Dtos
{
    public class OrderItemPictureUrlresolver : IValueResolver<OrderItem, OrderItemDto, string>
    {
        private readonly IConfiguration _configuration;

        public OrderItemPictureUrlresolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string Resolve(OrderItem source, OrderItemDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.ProductItem.ProductName))
            {
                return $"{_configuration["BaseUrl"]}/{source.ProductItem.ProductName}";
            }
            return null;
        }
    }
}
