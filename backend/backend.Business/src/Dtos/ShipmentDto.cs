using backend.Domain.src.Entities;

namespace backend.Business.src.Dtos
{
    public class ShipmentReadDto
    {
        public Guid Id { get; set; }
        public string CompanyShipmentName { get; set; }
        public string ShipmentTrackingNumnber { get; set; }
        public ShipmentState ShipmentState { get; set; }
        public int OrderId { get; set; }
    }

    public class ShipmentCreateDto
    {
        public string CompanyShipmentName { get; set; }
        public string ShipmentTrackingNumnber { get; set; }
        public ShipmentState ShipmentState { get; set; }
        public int OrderId { get; set; }
    }

    public class ShipmentUpdateDto
    {
        public ShipmentState ShipmentState { get; set; }
    }
}