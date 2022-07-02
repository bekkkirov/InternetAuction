import {Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {map, ReplaySubject} from "rxjs";
import {LoggedInUser} from "../models/logged-in-user.model";
import {LoginModel} from "../models/login.model";
import {RegisterModel} from "../models/register.model";
import {environment} from "../../../environments/environment.prod";

@Injectable({
    providedIn: 'root'
})
export class AccountService {
    private currentUserSource = new ReplaySubject<LoggedInUser | null>(1);
    currentUser$ = this.currentUserSource.asObservable();
    private apiUrl = environment.apiUrl + "auth/";

    constructor(private http: HttpClient) {
    }

    setCurrentUser(user: LoggedInUser | null) {
        if(user) {
            let userRoles = this.decodeToken(user.token)?.role;

            if(Array.isArray(userRoles)) {
                user.roles = userRoles;
            }

            else {
                user.roles = [];
                user.roles.push(userRoles)
            }
        }

        localStorage.setItem('user', JSON.stringify(user));
        this.currentUserSource.next(user);
    }

    signIn(model: LoginModel) {
        return this.http.post<LoggedInUser>(this.apiUrl + "sign-in", model).pipe(
            map((response: LoggedInUser) => {
                let user = response;

                if (user) {
                    this.setCurrentUser(user);
                }
            })
        );
    }

    signUp(model: RegisterModel) {
        return this.http.post<LoggedInUser>(this.apiUrl + "sign-up", model).pipe(
            map((response: LoggedInUser) => {
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

    decodeToken(token: string) {
        if(token) {
            return JSON.parse(atob(token.split('.')[1]));
        }
    }
}
