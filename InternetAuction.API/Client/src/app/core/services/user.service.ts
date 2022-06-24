import {Injectable} from '@angular/core';
import {environment} from "../../../environments/environment.prod";
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {User} from "../models/user.model";
import {UserUpdate} from "../models/user-update.model";
import {Image} from "../models/image.model";

@Injectable({
    providedIn: 'root'
})
export class UserService {
    apiUrl = environment.apiUrl + "users/";

    constructor(private http: HttpClient) {
    }

    getByUserName(userName: string) {
        return this.http.get<User>(this.apiUrl + userName);
    }

    addProfileImage(image) {
        let headers = new HttpHeaders();
        headers.append('Content-Disposition','multipart/form-data');

        let formData = new FormData();
        formData.append('image', image);

        return this.http.post<Image>(this.apiUrl + "edit/image", formData, {headers: headers});
    }

    updateProfile(model: UserUpdate) {
        return this.http.put(this.apiUrl + "edit/profile", model);
    }
}
