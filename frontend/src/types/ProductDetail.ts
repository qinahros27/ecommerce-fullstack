import { Guid } from "guid-typescript";
import Category from "./Category";

export default interface ProductDetail {
    id?: Guid,
    title: string,
    price: number,
    description: string,
    category: Category,
    images: string[]
}