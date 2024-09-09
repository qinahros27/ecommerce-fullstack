import { Guid } from "guid-typescript"

export default interface Product {
    id?: Guid
    title: string
    price: number
    description: string
    category: Guid
    images: string[]
}