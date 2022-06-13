import {Component, OnInit} from '@angular/core';
import {AccountService} from "./core/services/account.service";
import {Router} from "@angular/router";

@Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit{
    title = 'Client';

    constructor(private accountService: AccountService, public router: Router) {
    }

    ngOnInit(): void {
        let currentUser = JSON.parse(localStorage.getItem('user'));
        console.log(currentUser);
        this.accountService.setCurrentUser(currentUser);
    }
}
