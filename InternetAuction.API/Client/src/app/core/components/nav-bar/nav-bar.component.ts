import {Component, EventEmitter, OnInit, Output} from '@angular/core';
import {AccountService} from "../../services/account.service";
import {Router} from "@angular/router";
import {LoggedInUser} from "../../models/logged-in-user.model";
import {LotService} from "../../services/lot.service";

@Component({
    selector: 'app-nav-bar',
    templateUrl: './nav-bar.component.html',
    styleUrls: ['./nav-bar.component.css']
})
export class NavBarComponent implements OnInit {
    searchValue: string;
    currentUser: LoggedInUser;

    @Output() sideBarOpen = new EventEmitter<boolean>();

    constructor(public accountService: AccountService,
                private lotService: LotService,
                private router: Router) {
    }

    ngOnInit(): void {
        this.accountService.currentUser$.subscribe(result => this.currentUser = result);
    }

    searchForLots() {
        this.lotService.setSearchValue(this.searchValue);
    }

    toggleSideBar() {
        this.sideBarOpen.emit(true);
    }

    onLotsPage() {
        return this.router.url === '/' ||
            this.router.url.includes('categories/') ||
            this.router.url.includes('lots/search/');
    }
}
