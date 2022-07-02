import {Component, OnInit} from '@angular/core';
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

    constructor(public accountService: AccountService,
                private lotService: LotService) {
    }

    ngOnInit(): void {
        this.accountService.currentUser$.subscribe(result => this.currentUser = result);
    }

    searchForLots() {
        debugger;
        this.lotService.setSearchValue(this.searchValue);
    }
}
