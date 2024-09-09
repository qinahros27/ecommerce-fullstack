import { Guid } from "guid-typescript";

export default interface ShipmentCreate
{
    companyShipmentName: string
    shipmentTrackingNumber: string
    shipmentState: "Delivering"|"Success"|"Cancel"
    orderProductId: Guid
}

export default interface ShipmentRead
{
    id: Guid
    companyShipmentName: string
    shipmentTrackingNumber: string
    shipmentState: "Delivering" | "Success" | "Cancel"
    orderProductId: Guid
}

export default interface OrderProductShipmentRead
{
    companyShipmentName: string
    shipmentTrackingNumber: string
    shipmentState: "Delivering" | "Success" | "Cancel"
}
