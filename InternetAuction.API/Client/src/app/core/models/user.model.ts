import {LotModel} from "./lot.model";
import {BidModel} from "./bid.model";
import {ImageModel} from "./image.model";

export interface UserModel {
    id: number,
    userName: string,
    firstName: string,
    lastName: string,
    balance: string,
    profileImage: ImageModel,
    registeredLots: LotModel[],
    boughtLots: LotModel[],
    bids: BidModel[]
}