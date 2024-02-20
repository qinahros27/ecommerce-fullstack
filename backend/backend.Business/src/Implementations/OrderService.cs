using AutoMapper;
using backend.Business.src.Abstractions;
using backend.Business.src.Dtos;
using backend.Domain.src.Entities;
using backend.Domain.src.Abstractions;

namespace backend.Business.src.Implementations
{
    public class OrderService : BaseService<Order, OrderReadDto, OrderCreateDto, OrderUpdateDto>, IOrderService
    {
        private readonly IOrderRepo _orderRepo;
        private readonly IProductRepo _productRepo;
        private readonly ICategoryRepo _categoryRepo;
        private readonly IOrderProductRepo _orderProductRepo;
        public OrderService(IOrderRepo orderRepo,IProductRepo productRepo,IOrderProductRepo orderProductRepo,ICategoryRepo categoryRepo, IMapper mapper) : base(orderRepo, mapper)
        {
            _orderRepo = orderRepo;
            _orderProductRepo = orderProductRepo;
            _productRepo = productRepo;
            _categoryRepo = categoryRepo;
        }

        public async Task<IEnumerable<UserOrdersReadDto>> GetAllByUserId(Guid userId)
        {
            var userOrderList = await _orderRepo.GetAllByUserId(userId);

            foreach(var userOrder in userOrderList)
            {
                var orderProducts = await _orderProductRepo.GetAllByOrderId(userOrder.Id);
                userOrder.OrderProducts = orderProducts.ToList();
                foreach(var product in userOrder.OrderProducts)
                {
                    product.Product = await _productRepo.GetOneById(product.ProductId);
                    product.Product.Category = await _categoryRepo.GetOneById(product.Product.CategoryId);
                }
            }

            return userOrderList.Select(userOrder => new UserOrdersReadDto
            {
                OrderProducts = _mapper.Map<List<OrderOfOrderProductReadDto>>(userOrder.OrderProducts)
            });
        }
    }
}