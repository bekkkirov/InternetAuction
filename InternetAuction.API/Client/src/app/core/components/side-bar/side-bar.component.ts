import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {AccountService} from "../../services/account.service";
import {LoggedInUser} from "../../models/logged-in-user.model";
import {Router} from "@angular/router";

@Component({
    selector: 'app-side-bar',
    templateUrl: './side-bar.component.html',
    styleUrls: ['./side-bar.component.css']
})
export class SideBarComponent implements OnInit {
    currentUser: LoggedInUser;
    @Input() sideBarOpen: boolean = false;
    @Output() sideBarClose = new EventEmitter<boolean>();

    constructor(private accountService: AccountService, private router: Router) {
    }

    ngOnInit(): void {
        this.accountService.currentUser$.subscribe(result => this.currentUser = result);
    }

    logout() {
        this.accountService.logout();
        this.router.navigateByUrl("/");
    }

    closeSideBar() {
        this.sideBarClose.emit(false);
    }

}
