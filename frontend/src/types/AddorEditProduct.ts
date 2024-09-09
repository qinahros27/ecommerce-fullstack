import { Guid } from "guid-typescript"

export default interface AddorEditProduct {
    title: string
    price: number
    description: string
    categoryId: Guid 
    inventory: number
    images: string[]
}