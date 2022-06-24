import {Lot} from "./lot.model";

export interface LotCategory {
    id: number,
    name: string,
    lots: Lot[]
}