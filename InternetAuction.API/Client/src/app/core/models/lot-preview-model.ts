import {Image} from "./image.model";

export interface LotPreview {
    id: number,
    name: string,
    saleEndTime: Date,
    image: Image,
    bidCount: number,
    currentPrice: number
}
