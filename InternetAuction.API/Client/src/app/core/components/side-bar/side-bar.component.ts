import {Component, OnInit} from '@angular/core';
import {AccountService} from "../../services/account.service";
import {LoggedInUser} from "../../models/logged-in-user.model";
import {Router} from "@angular/router";
import {take} from "rxjs";

@Component({
    selector: 'app-side-bar',
    templateUrl: './side-bar.component.html',
    styleUrls: ['./side-bar.component.css']
})
export class SideBarComponent implements OnInit {
    currentUser: LoggedInUser;

    constructor(private accountService: AccountService, private router: Router) {
    }

    ngOnInit(): void {
        this.accountService.currentUser$.pipe(take(1))
                                        .subscribe(result => this.currentUser = result);
    }

    logout() {
        this.accountService.logout();
        this.router.navigateByUrl("/");
    }

}
