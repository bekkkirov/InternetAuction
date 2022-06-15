import {LotModel} from "./lot.model";

export interface LotCategoryModel {
    id: number,
    name: string,
    lots: LotModel[]
}