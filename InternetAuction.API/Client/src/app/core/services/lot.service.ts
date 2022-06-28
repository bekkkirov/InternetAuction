import {Injectable} from '@angular/core';
import {HttpClient, HttpHeaders, HttpParams} from "@angular/common/http";
import {LotCategory} from "../models/lot-category.model";
import {environment} from "../../../environments/environment.prod";
import {LotPreview} from "../models/lot-preview-model";
import {map} from "rxjs";
import {PaginatedResult} from "../models/paginated-result.model";
import {Lot} from "../models/lot.model";
import {LotParameters} from "../models/lot-parameters.model";
import {LotCreate} from "../models/lot-create.model";
import {Image} from "../models/image.model";
import {BidCreate} from "../models/bid-create.model";

@Injectable({
    providedIn: 'root'
})
export class LotService {
    apiUrl = environment.apiUrl + "lots/"

    constructor(private http: HttpClient) {
    }

    getLot(lotId: number) {
        return this.http.get<Lot>(this.apiUrl + lotId);
    }

    addLot(lot: LotCreate) {
        return this.http.post<Lot>(this.apiUrl, lot);
    }

    getCategories() {
        return this.http.get<LotCategory[]>(this.apiUrl + "categories");
    }

    getLotPreviews(lotParams: LotParameters) {
        let params: HttpParams = this.addParams(lotParams);

        return this.http.get<LotPreview[]>(this.apiUrl + "previews", {observe: 'response', params: params}).pipe(
            map(response => {
                return new PaginatedResult<LotPreview[]>(response.body, JSON.parse(response.headers.get('pagination')));
            })
        );
    }

    getLotsByCategory(categoryId, lotParams: LotParameters) {
        let params: HttpParams = this.addParams(lotParams);

        return this.http.get<LotPreview[]>(this.apiUrl + "categories/" + categoryId, {observe: 'response', params: params}).pipe(
            map(response => {
                return new PaginatedResult<LotPreview[]>(response.body, JSON.parse(response.headers.get('pagination')));
            })
        );
    }

    addParams(lotParams: LotParameters) {
        let params: HttpParams = new HttpParams();

        params = params.append('pageNumber', lotParams.pageNumber.toString());
        params = params.append('minPrice', lotParams.minPrice);
        params = params.append('orderOptions', lotParams.orderOptions);

        if(lotParams.maxPrice != null) {
            params = params.append('maxPrice', lotParams.maxPrice);
        }

        return params;
    }

    addLotImage(image, lotId: number) {
        let headers = new HttpHeaders();
        headers.append('Content-Disposition','multipart/form-data');

        let formData = new FormData();
        formData.append('image', image);

        return this.http.post<Image>(this.apiUrl + `${lotId}/image`, formData, {headers: headers});
    }

    deleteLot(lotId: number) {
        return this.http.delete(this.apiUrl + lotId);
    }

    placeBid(lotId: number, bid: BidCreate) {
        return this.http.post(this.apiUrl + `${lotId}/bids`, bid);
    }

    search(searchValue: string, lotParams: LotParameters) {
        let params: HttpParams = this.addParams(lotParams);

        return this.http.get<LotPreview[]>(this.apiUrl + `search/${searchValue}`, {observe: 'response', params: params}).pipe(
            map(response => {
                return new PaginatedResult<LotPreview[]>(response.body, JSON.parse(response.headers.get('pagination')));
            })
        );
    }
}
