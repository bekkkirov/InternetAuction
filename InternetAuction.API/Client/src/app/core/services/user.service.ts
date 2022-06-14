import {Injectable} from '@angular/core';
import {environment} from "../../../environments/environment.prod";
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {UserModel} from "../models/UserModel";
import {UserUpdateModel} from "../models/UserUpdateModel";
import {ImageModel} from "../models/ImageModel";

@Injectable({
    providedIn: 'root'
})
export class UserService {
    apiUrl = environment.apiUrl + "users/";

    constructor(private http: HttpClient) {
    }

    getByUserName(userName: string) {
        return this.http.get<UserModel>(this.apiUrl + userName);
    }

    addProfileImage(image) {
        let headers = new HttpHeaders();
        headers.append('Content-Disposition','multipart/form-data');

        return this.http.post<ImageModel>(this.apiUrl + "edit/image", image, {headers: headers});
    }

    updateProfile(model: UserUpdateModel) {
        return this.http.put(this.apiUrl + "edit/profile", model);
    }
}
