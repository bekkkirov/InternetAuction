import {Lot} from "./lot.model";
import {Bid} from "./bid.model";
import {Image} from "./image.model";

export interface User {
    id: number,
    userName: string,
    firstName: string,
    lastName: string,
    balance: string,
    profileImage: Image,
    registeredLots: Lot[],
    boughtLots: Lot[],
    bids: Bid[]
}