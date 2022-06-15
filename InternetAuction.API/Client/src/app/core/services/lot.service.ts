import {Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {LotCategoryModel} from "../models/LotCategoryModel";
import {environment} from "../../../environments/environment.prod";

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
}
