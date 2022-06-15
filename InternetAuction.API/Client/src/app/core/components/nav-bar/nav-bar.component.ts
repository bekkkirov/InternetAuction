import {Component, OnInit} from '@angular/core';
import {AccountService} from "../../services/account.service";
import {LoggedInUserModel} from "../../models/logged-in-user.model";
import {Router} from "@angular/router";

@Component({
    selector: 'app-nav-bar',
    templateUrl: './nav-bar.component.html',
    styleUrls: ['./nav-bar.component.css']
})
export class NavBarComponent implements OnInit {
    constructor(public accountService: AccountService, private router: Router) {
    }

    ngOnInit(): void {

    }

    logout() {
        this.accountService.logout();
        this.router.navigateByUrl("/");
    }
}
