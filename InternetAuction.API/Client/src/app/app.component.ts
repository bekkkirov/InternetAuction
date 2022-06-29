import {Component, OnInit} from '@angular/core';
import {AccountService} from "./core/services/account.service";
import {Router} from "@angular/router";
import {take} from "rxjs";
import {LoggedInUser} from "./core/models/logged-in-user.model";

@Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit{
    title = 'Auction';
    currentUser: LoggedInUser;

    constructor(public accountService: AccountService, public router: Router) {
    }

    ngOnInit(): void {
        let currentUser = JSON.parse(localStorage.getItem('user'));
        this.accountService.setCurrentUser(currentUser);

        this.accountService.currentUser$.subscribe(result => this.currentUser = result);
    }

    toggleGrid() {
        if(this.onLotsPage()) {
            return 'gridA';
        }

        if(!this.currentUser && !this.router.url.includes('auth')) {
            return 'gridB';
        }

        return '';
    }

    toggleSideBar() {
        return this.currentUser && this.onLotsPage();
    }

    toggleNavBar() {
        return this.router.url != '/auth/sign-in' && this.router.url != '/auth/sign-up'
    }

    onLotsPage() {
        return this.router.url === '/' ||
            this.router.url.includes('categories/') ||
            this.router.url.includes('lots/search/');
    }
}
