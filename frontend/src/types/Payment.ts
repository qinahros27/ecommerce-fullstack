import { Guid } from "guid-typescript";

export default interface PaymentRead 
{
    id: Guid
    paymentMethod: "Card" | "PayPal" | "OnlinePayment"
    totalPrice: number
    orderId: Guid
}

export default interface PaymentCreate
{
    paymentMethod: "Card" | "PayPal" | "OnlinePayment"
    totalPrice: number
    orderId: Guid
}