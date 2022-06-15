import {LotModel} from "./lot.model";
import {BidModel} from "./bid.model";

export interface UserModel {
    id: number,
    userName: string,
    firstName: string,
    lastName: string,
    balance: string
    profileImageId: number
    registeredLots: LotModel[]
    boughtLots: LotModel[]
    bids: BidModel[]
}