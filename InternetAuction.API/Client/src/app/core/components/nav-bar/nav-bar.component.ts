import {Component, OnInit} from '@angular/core';
import {AccountService} from "../../services/account.service";
import {LoggedInUserModel} from "../../models/LoggedInUserModel";

@Component({
    selector: 'app-nav-bar',
    templateUrl: './nav-bar.component.html',
    styleUrls: ['./nav-bar.component.css']
})
export class NavBarComponent implements OnInit {
    constructor(public accountService: AccountService) {
    }

    ngOnInit(): void {

    }

    logout() {

    }
}
