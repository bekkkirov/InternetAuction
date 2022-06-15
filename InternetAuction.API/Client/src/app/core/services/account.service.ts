import {Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {map, ReplaySubject} from "rxjs";
import {LoggedInUserModel} from "../models/logged-in-user.model";
import {LoginModel} from "../models/login.model";
import {RegisterModel} from "../models/register.model";
import {environment} from "../../../environments/environment.prod";

@Injectable({
    providedIn: 'root'
})
export class AccountService {
    private currentUserSource = new ReplaySubject<LoggedInUserModel | null>(1);
    currentUser$ = this.currentUserSource.asObservable();
    private apiUrl = environment.apiUrl + "auth/";

    constructor(private http: HttpClient) {
    }

    setCurrentUser(user: LoggedInUserModel | null) {
        localStorage.setItem('user', JSON.stringify(user));
        this.currentUserSource.next(user);
    }

    signIn(model: LoginModel) {
        return this.http.post<LoggedInUserModel>(this.apiUrl + "sign-in", model).pipe(
            map((response: LoggedInUserModel) => {
                let user = response;

                if (user) {
                    this.setCurrentUser(user);
                }
            })
        );
    }

    signUp(model: RegisterModel) {
        return this.http.post<LoggedInUserModel>(this.apiUrl + "sign-up", model).pipe(
            map((response: LoggedInUserModel) => {
                let user = response;

                if (user) {
                    this.setCurrentUser(user);
                }
            })
        );
    }

    logout() {
        localStorage.removeItem('user');
        this.setCurrentUser(null);
    }
}
