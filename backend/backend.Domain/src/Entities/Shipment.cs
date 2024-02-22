using System.Text.Json.Serialization;

namespace backend.Domain.src.Entities
{
    public class Shipment : BaseEntity
    {
        public string CompanyShipmentName { get; set; }
        public string ShipmentTrackingNumnber { get; set; }
        public ShipmentState ShipmentState { get; set; }
        public Guid OrderProductId { get; set; }

        public OrderProduct OrderProduct { get; set; }
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
     public enum ShipmentState
    {
        Delivering,
        Success,
        Cancel
    }
}