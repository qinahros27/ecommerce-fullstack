import { Guid } from "guid-typescript"
import Product from "./Product"
import OrderProductShipmentRead from "./Shipment" 

export interface OrderProductCreate
{
    orderId: Guid
    productId: Guid
    quantity: number
    color?: string
}

export interface OrderProductRead
{
    id: Guid
    orderId: Guid
    productId: Guid
    quantity: number
    color?: string
}

export interface OrderOfOrderProductRead
{
    product: Product
    quantity: number
    color?: string
    shipment: OrderProductShipmentRead
}

