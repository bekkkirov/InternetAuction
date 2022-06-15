import {Injectable} from '@angular/core';
import {
    HttpRequest,
    HttpHandler,
    HttpEvent,
    HttpInterceptor
} from '@angular/common/http';
import {Observable, take} from 'rxjs';
import {LoggedInUserModel} from "../models/logged-in-user.model";
import {AccountService} from "../services/account.service";

@Injectable()
export class TokenInterceptor implements HttpInterceptor {

    constructor(private accountService: AccountService) {
    }

    intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
        let user: LoggedInUserModel | null;
        this.accountService.currentUser$.pipe(take(1)).subscribe(currentUser => user = currentUser);

        if(user) {
            request = request.clone({
                setHeaders: {
                    Authorization: `Bearer ${user.token}`
                }
            });
        }

        return next.handle(request);
    }
}
