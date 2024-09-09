import { Guid } from "guid-typescript"

export default interface Category {
    id?: Guid
    name: string
    description: string
    image: string
}