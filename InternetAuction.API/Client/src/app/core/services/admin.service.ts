import {Injectable} from '@angular/core';
import {environment} from "../../../environments/environment.prod";
import {HttpClient, HttpParams} from "@angular/common/http";
import {CategoryCreate} from "../models/category-create.model";

@Injectable({
    providedIn: 'root'
})
export class AdminService {
    apiUrl: string = environment.apiUrl + 'admin/';

    constructor(private http: HttpClient) {
    }

    createCategory(model: CategoryCreate) {
        return this.http.post(this.apiUrl + 'categories/create', model);
    }

    assignRole(userName: string, roleName: string) {
        let params: HttpParams = new HttpParams();
        params = params.append('userName', userName);
        params = params.append('roleName', roleName);

        return this.http.put(this.apiUrl + 'users/assign-role', null, { params });
    }
}

