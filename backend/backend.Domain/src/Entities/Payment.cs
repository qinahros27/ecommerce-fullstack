using System.Text.Json.Serialization;

namespace backend.Domain.src.Entities
{
    public class Payment : BaseEntity
    {
        public PaymentMethod PaymentMethod { get; set; }
        public float TotalPrice { get; set; }
        public Guid OrderId { get; set; }

        public Order Order { get; set; }
    }
    
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum PaymentMethod
    {
        Card,
        PayPal,
        OnlinePayment
    }
}