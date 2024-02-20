using AutoMapper;
using backend.Business.src.Dtos;
using backend.Domain.src.Entities;

namespace backend.Infrastructure.src.Configuration
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<User, UserReadDto>();
            CreateMap<UserUpdateDto, User>();
            CreateMap<UserCreateDto, User>();

            CreateMap<Product, ProductReadDto>();
            CreateMap<ProductCreateDto, Product>();
            CreateMap<ProductUpdateDto, Product>();

            CreateMap<Category, CategoryReadDto>();
            CreateMap<CategoryCreateDto, Category>();
            CreateMap<CategoryUpdateDto, Category>();

            CreateMap<Order, OrderReadDto>();
            CreateMap<OrderCreateDto, Order>();
            CreateMap<OrderUpdateDto, Order>();

            CreateMap<OrderProduct, OrderProductReadDto>();
            CreateMap<OrderProductCreateDto, OrderProduct>();
            CreateMap<OrderProductUpdateDto, OrderProduct>();
            CreateMap<OrderProduct, OrderOfOrderProductReadDto>();

            CreateMap<Payment, PaymentReadDto>();
            CreateMap<PaymentCreateDto, Payment>();
            CreateMap<PaymentUpdateDto, Payment>();

            CreateMap<ReviewRate, ReviewRateReadDto>();
            CreateMap<ReviewRateCreateDto, ReviewRate>();
            CreateMap<ReviewRateUpdateDto, ReviewRate>();
            CreateMap<ReviewRate, UserReviewRateReadDto>();
            CreateMap<ReviewRate, ProductReviewRateReadDto>();

            CreateMap<Shipment, ShipmentReadDto>();
            CreateMap<ShipmentCreateDto, Shipment>();
            CreateMap<ShipmentUpdateDto, Shipment>();

            CreateMap<UserCard, UserCardReadDto>();
            CreateMap<UserCardCreateDto, UserCard>();
            CreateMap<UserCardUpdateDto, UserCard>();
        }
    }
}