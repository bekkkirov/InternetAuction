import {ImageModel} from "./image.model";

export interface LotPreviewModel {
    id: number,
    name: string,
    saleEndTime: Date,
    image: ImageModel,
    bidCount: number,
    currentPrice: number
}
