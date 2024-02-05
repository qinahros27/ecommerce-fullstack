using System.Text.Json.Serialization;

namespace backend.Domain.src.Entities
{
    public class Shipment : BaseEntity
    {
        public string CompanyShipmentName { get; set; }
        public string ShipmentTrackingNumnber { get; set; }
        public ShipmentState ShipmentState { get; set; }
        public Guid OrderId { get; set; }

        public Order Order { get; set; }
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
     public enum ShipmentState
    {
        Delivering,
        Success,
        Cancel
    }
}