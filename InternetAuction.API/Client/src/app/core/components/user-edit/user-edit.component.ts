import {Component, OnInit} from '@angular/core';
import {AccountService} from "../../services/account.service";
import {LoggedInUserModel} from "../../models/LoggedInUserModel";
import {take} from "rxjs";

@Component({
    selector: 'app-user-edit',
    templateUrl: './user-edit.component.html',
    styleUrls: ['./user-edit.component.css']
})
export class UserEditComponent implements OnInit {
    user: LoggedInUserModel;

    constructor(private accountService: AccountService) {
        this.accountService.currentUser$.pipe(take(1)).subscribe(currentUser => this.user = currentUser);
    }

    ngOnInit(): void {
    }

}
