﻿using Microsoft.AspNetCore.Mvc;
using Store.Repository.Interfaces;
using Store.Repository.Repositories;
using Store.Service.Services.Product.Dtos;
using Store.Service.Services.Product;
using Store.Service.HandleResponse;
using Store.Service.Services.CacheServices;
using Store.Service.Services.BasketServices.Dto;
using Store.Service.Services.BasketServices;
using Store.Repository.Basket;
using Store.Service.Services.TokenServices;
using Store.Service.Services.userService;
using Store.Service.Services.OrderServices.Dtos;
using Store.Service.Services.OrderServices;
using Store.Service.Services.PaymentService;

namespace Store.Web.Extentions
{
    public static  class ApplicationServiceExtentions
    {
        public static IServiceCollection AddAlicationServices(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICacheService, Cacheservices>();
            services.AddScoped<ICacheService, Cacheservices>();
            services.AddScoped<ITokenServices, TokenServices>();
            services.AddScoped<IUserService, UserServices>();
            services.AddScoped<IBasketRepository, BasketRepository>();
            services.AddScoped<IOrderServices, OrderServices>();
            services.AddScoped<IOrderServices, OrderServices>();
            services.AddScoped<IOrderServices, OrderServices>();
            services.AddAutoMapper(typeof(ProductProfile));
            services.AddAutoMapper(typeof(BasketProfile));
            services.AddAutoMapper(typeof(OrderProfile));
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = actionContext =>
                {
                    var errors = actionContext.ModelState
                    .Where(model => model.Value?.Errors.Count > 0)
                    .SelectMany(model => model.Value?.Errors)
                    .Select(error => error.ErrorMessage)
                    .ToList();

                    var errorResponse = new ValidationErrorResponse
                    {
                        Errors = errors
                    };
                    return new BadRequestObjectResult(errorResponse);
                };
            });
            return services;
        }
    }
}
