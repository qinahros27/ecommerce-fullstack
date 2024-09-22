import { Guid } from "guid-typescript"
import { OrderOfOrderProductRead } from "./OrderProduct"

export interface OrderCreate {
    address: string
    postCode: string
    city: string
    country: string
    phoneNumber: string
    userId: Guid
}

export interface OrderRead {
    id?: Guid
    postCode: string
    city: string
    country: string
    phoneNumber: string
    userId: Guid
}

export interface UserOrdersRead
{
    orderProducts: OrderOfOrderProductRead[]
}