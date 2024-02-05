using AutoMapper;
using backend.Business.src.Abstractions;
using backend.Business.src.Dtos;
using backend.Domain.src.Entities;
using backend.Domain.src.Abstractions;

namespace backend.Business.src.Implementations
{
    public class PaymentService : BaseService<Payment, PaymentReadDto, PaymentCreateDto, PaymentUpdateDto>, IPaymentService
    {
        public PaymentService(IPaymentRepo paymentRepo, IMapper mapper) : base(paymentRepo, mapper)
        {
        }
    }
}