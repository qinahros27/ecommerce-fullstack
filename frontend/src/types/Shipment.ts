import { Guid } from "guid-typescript";

export interface ShipmentCreate
{
    companyShipmentName: string
    shipmentTrackingNumber: string
    shipmentState: "Delivering"|"Success"|"Cancel"
    orderProductId: Guid
}

export interface ShipmentRead
{
    id: Guid
    companyShipmentName: string
    shipmentTrackingNumber: string
    shipmentState: "Delivering" | "Success" | "Cancel"
    orderProductId: Guid
}

export interface OrderProductShipmentRead
{
    companyShipmentName: string
    shipmentTrackingNumber: string
    shipmentState: "Delivering" | "Success" | "Cancel"
}
