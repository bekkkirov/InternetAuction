import {Injectable} from '@angular/core';
import {CanActivate, Router} from '@angular/router';
import {map, Observable} from 'rxjs';
import {AccountService} from "../services/account.service";

@Injectable({
    providedIn: 'root'
})
export class AuthGuard implements CanActivate {
    constructor(private accountService: AccountService, private router: Router) {
    }

    canActivate(): Observable<boolean> {
        return this.accountService.currentUser$.pipe(
            map(currentUser => {
                if(currentUser) {
                    return true;
                }

                this.router.navigateByUrl('/auth/sign-in');
                return false;
            })
        );
    }
}
