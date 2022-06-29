import {Injectable} from '@angular/core';
import {ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree} from '@angular/router';
import {map, Observable} from 'rxjs';
import {AccountService} from "../services/account.service";

@Injectable({
    providedIn: 'root'
})
export class ModerGuard implements CanActivate {

    constructor(private accountService: AccountService, private router: Router) {
    }

    canActivate(): Observable<boolean> {
        return  this.accountService.currentUser$.pipe(
            map(user => {
                if(user?.roles?.includes('Moderator')) {
                    return true;
                }

                this.router.navigateByUrl('/');
                return false;
            })
        )
    }

}
