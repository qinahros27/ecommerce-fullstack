import { Guid } from "guid-typescript";

export default interface User {
    id?: Guid,
    email: string,
    password: string,
    firstName: string,
    lastName: string,
    userName: string,
    address?: string,
    post_code?: string,
    city?: string,
    country?: string,
    role?: "Customer" | "Admin",
    avatar: string
}