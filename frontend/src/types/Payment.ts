import { Guid } from "guid-typescript";

export interface PaymentRead 
{
    id: Guid
    paymentMethod: "Card" | "PayPal" | "OnlinePayment"
    totalPrice: number
    orderId: Guid
}

export interface PaymentCreate
{
    paymentMethod: "Card" | "PayPal" | "OnlinePayment"
    totalPrice: number
    orderId: Guid
}