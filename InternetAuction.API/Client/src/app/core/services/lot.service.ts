import {Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {LotCategoryModel} from "../models/lot-category.model";
import {environment} from "../../../environments/environment.prod";
import {LotPreviewModel} from "../models/lot-preview-model";

@Injectable({
    providedIn: 'root'
})
export class LotService {
    apiUrl = environment.apiUrl + "lots/"

    constructor(private http: HttpClient) {
    }

    getCategories() {
        return this.http.get<LotCategoryModel[]>(this.apiUrl + "categories");
    }

    getLotPreviews() {
        return this.http.get<LotPreviewModel[]>(this.apiUrl + "previews");
    }
}
