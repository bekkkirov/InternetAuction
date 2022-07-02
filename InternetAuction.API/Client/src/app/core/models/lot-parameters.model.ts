export class LotParameters {
    pageNumber: number = 1;
    minPrice: number = 0;
    maxPrice?: number;
    searchValue: string;
    orderOptions: string = "PriceAscending";
}
