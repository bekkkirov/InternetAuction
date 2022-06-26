export interface Bid {
    id: number,
    bidValue: number,
    bidTime: Date,
    lotId: number,
    lotName: string,
    bidderId: number
}