using AutoMapper;
using Core.Dtos;
using Core.Dtos.Baskets;
using Core.Dtos.Orders;
using Core.Dtos.ProductDtos;
using Core.Dtos.Users;
using Core.Models;
using E.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Mapping
{
    public class MapProfile:Profile
    {
        public MapProfile()
        {
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<ProductUpdateDto, Product>();
            CreateMap<Product, ProductWithCategoryDto>(); ;
            // CreateMap<Category, CategoryWithProductDto>().ReverseMap();
            CreateMap<ProductCreateDto, Product>().ReverseMap();
            CreateMap<CreateCategoryDto, Category>();
            CreateMap<Order, OrderDto>().ReverseMap();
            CreateMap<Basket, BasketDto>().ReverseMap();
            CreateMap<BasketItem, BasketItemDto>().ReverseMap();
            CreateMap<CreateOrderDto, Order>();
            CreateMap<UserAppDto, UserApp>().ReverseMap();
            CreateMap<CreateUserDto, UserApp>().ReverseMap();
            CreateMap<ResetPasswordDto, UserApp>();
        }
        

    }
}
