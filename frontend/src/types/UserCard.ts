import { Guid } from "guid-typescript"

export interface UserCardRead
{
    id: Guid
    cardName: string
    type: string
    cardNumber: string
    expiredDate: Date
    userId: Guid
}

export interface UserCardCreate
{
    cardName: string
    type: string
    cardNumber: string
    expiredDate: Date
    cvv: number
    userId: Guid
}