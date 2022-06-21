import {ImageModel} from "./image.model";

export interface LotModel {
    id: number,
    name: string,
    description: string,
    currentPrice: number,
    saleEndTime: Date,
    quantity: number,
    categoryId: number,
    sellerId: number,
    buyerId: number,
    images: ImageModel[]
}
