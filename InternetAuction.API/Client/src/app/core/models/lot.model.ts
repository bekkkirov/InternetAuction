import {Image} from "./image.model";

export interface Lot {
    id: number,
    name: string,
    description: string,
    currentPrice: number,
    saleEndTime: Date,
    quantity: number,
    categoryId: number,
    sellerUserName: string,
    buyerId: number,
    images: Image[]
}
