import {Component, OnInit} from '@angular/core';
import {AccountService} from "../../services/account.service";
import {Router} from "@angular/router";
import {LoggedInUser} from "../../models/logged-in-user.model";

@Component({
    selector: 'app-nav-bar',
    templateUrl: './nav-bar.component.html',
    styleUrls: ['./nav-bar.component.css']
})
export class NavBarComponent implements OnInit {
    searchValue: string;
    currentUser: LoggedInUser;

    constructor(public accountService: AccountService,
                private router: Router) {
    }

    ngOnInit(): void {
        this.accountService.currentUser$.subscribe(result => this.currentUser = result);
    }

    searchForLots() {
        if(this.searchValue) {
            this.router.navigateByUrl('lots/search/' + this.searchValue);
        }

    }
}
