import {Image} from "./image.model";

export interface Lot {
    id: number,
    name: string,
    description: string,
    currentPrice: number,
    saleEndTime: Date,
    quantity: number,
    categoryId: number,
    sellerId: number,
    buyerId: number,
    images: Image[]
}
