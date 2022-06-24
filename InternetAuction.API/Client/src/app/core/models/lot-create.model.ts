export interface LotCreate {
    name: string,
    description: string
    initialPrice: number
    saleEndTime: Date
    quantity: number
    categoryId: number
}
