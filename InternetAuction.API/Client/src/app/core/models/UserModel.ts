import {LotModel} from "./LotModel";
import {BidModel} from "./BidModel";

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