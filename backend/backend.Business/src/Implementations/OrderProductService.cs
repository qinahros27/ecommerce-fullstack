using AutoMapper;
using backend.Business.src.Abstractions;
using backend.Business.src.Dtos;
using backend.Domain.src.Entities;
using backend.Domain.src.Abstractions;

namespace backend.Business.src.Implementations
{
    public class OrderProductService : BaseService<OrderProduct, OrderProductReadDto, OrderProductCreateDto, OrderProductUpdateDto>, IOrderProductService
    {
        private readonly IOrderProductRepo _orderProductRepo;
        private readonly IProductRepo _productRepo;
        private readonly ICategoryRepo _categoryRepo;
        private readonly IShipmentRepo _shipmentRepo;
        public OrderProductService(IOrderProductRepo orderProductRepo,IProductRepo productRepo,ICategoryRepo categoryRepo,IShipmentRepo shipmentRepo, IMapper mapper) : base(orderProductRepo, mapper)
        {
            _orderProductRepo = orderProductRepo;
            _productRepo = productRepo;
            _categoryRepo = categoryRepo;
            _shipmentRepo = shipmentRepo;
        }

        public async Task<IEnumerable<OrderOfOrderProductReadDto>> GetAllByOrderId(Guid orderId)
        {
            var orderProductList = await _orderProductRepo.GetAllByOrderId(orderId);

            foreach(var orderProduct in orderProductList)
            {
                orderProduct.Product = await _productRepo.GetOneById(orderProduct.ProductId);
                orderProduct.Product.Category = await _categoryRepo.GetOneById(orderProduct.Product.CategoryId);
                orderProduct.Shipment = await _shipmentRepo.GetOneByOrderProductId(orderProduct.Id);
            }

            return _mapper.Map<IEnumerable<OrderOfOrderProductReadDto>>(orderProductList);
        }
    }
}