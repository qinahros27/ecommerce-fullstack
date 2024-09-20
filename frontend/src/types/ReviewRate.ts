import { Guid } from "guid-typescript";
import User from "./User";
import ProductDetail from "./ProductDetail";

export interface ReviewRateCreate
{
    userId: Guid
    productId: Guid
    review?: string
    ratePoint?: number
}

export interface ProductReviewRateRead
{
    user: User
    review?: string
    ratePoint?: number
}

export interface UserReviewRateRead
{
    product: ProductDetail
    review?: string
    ratePoint?: number
}

export interface ReviewRateRead
{
    productId: Guid
    review?: string
    ratePoint?: number
}
