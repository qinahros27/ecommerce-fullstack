import { Guid } from "guid-typescript";
import User from "./User";
import ProductDetail from "./ProductDetail";

export default interface ReviewRateCreate
{
    userId: Guid
    productId: Guid
    review?: string
    ratePoint?: number
}

export default interface ProductReviewRateRead
{
    user: User
    review?: string
    ratePoint?: number
}

export default interface UserReviewRateRead
{
    product: ProductDetail
    review?: string
    ratePoint?: number
}
