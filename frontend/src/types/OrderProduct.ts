import { Guid } from "guid-typescript"
import Product from "./Product"
import OrderProductShipmentRead from "./Shipment" 

export default interface OrderProductCreate
{
    orderId: Guid
    productId: Guid
    quantity: number
    color?: string
}

export default interface OrderProductRead
{
    id: Guid
    orderId: Guid
    productId: Guid
    quantity: number
    color?: string
}

export default interface OrderOfOrderProductRead
{
    product: Product
    quantity: number
    color?: string
    shipment: OrderProductShipmentRead
}

