using backend.Domain.src.Entities;
using backend.Business.src.Dtos;

namespace backend.Business.src.Abstractions
{
    public interface IPaymentService : IBaseService<Payment, PaymentReadDto, PaymentCreateDto, PaymentUpdateDto>
    {
    }
}