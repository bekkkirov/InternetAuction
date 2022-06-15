import {ImageModel} from "./image.model";

export interface LotModel {
    id: number,
    name: string,
    description: string,
    initialPrice: number,
    saleStartTime: Date,
    saleEndTime: Date,
    status: string,
    quantity: number,
    categoryId: number,
    categoryName: string,
    sellerId: number,
    buyerId: number,
    images: ImageModel[]
}
