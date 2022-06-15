import {LotModel} from "./LotModel";

export interface LotCategoryModel {
    id: number,
    name: string,
    lots: LotModel[]
}