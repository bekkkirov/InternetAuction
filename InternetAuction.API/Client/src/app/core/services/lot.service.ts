import {Injectable} from '@angular/core';
import {HttpClient, HttpParams} from "@angular/common/http";
import {LotCategoryModel} from "../models/lot-category.model";
import {environment} from "../../../environments/environment.prod";
import {LotPreviewModel} from "../models/lot-preview-model";
import {map} from "rxjs";
import {PaginatedResultModel} from "../models/paginated-result.model";
import {LotModel} from "../models/lot.model";
import {LotParameters} from "../models/lot-parameters.model";

@Injectable({
    providedIn: 'root'
})
export class LotService {
    apiUrl = environment.apiUrl + "lots/"
    paginatedResult: PaginatedResultModel<LotPreviewModel[]> = new PaginatedResultModel<LotPreviewModel[]>();

    constructor(private http: HttpClient) {
    }

    getCategories() {
        return this.http.get<LotCategoryModel[]>(this.apiUrl + "categories");
    }

    getLotPreviews(lotParams: LotParameters) {
        let params: HttpParams = this.addParams(lotParams);

        return this.http.get<LotPreviewModel[]>(this.apiUrl + "previews", {observe: 'response', params: params}).pipe(
            map(response => {
                this.paginatedResult.result = response.body;
                this.paginatedResult.pagination = JSON.parse(response.headers.get('pagination'));

                return this.paginatedResult;
            })
        );
    }

    getLotsByCategory(categoryId, lotParams: LotParameters) {
        let params: HttpParams = this.addParams(lotParams);

        return this.http.get<LotPreviewModel[]>(this.apiUrl + "categories/" + categoryId, {observe: 'response', params: params}).pipe(
            map(response => {
                this.paginatedResult.result = response.body;
                this.paginatedResult.pagination = JSON.parse(response.headers.get('pagination'));

                return this.paginatedResult;
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
}
