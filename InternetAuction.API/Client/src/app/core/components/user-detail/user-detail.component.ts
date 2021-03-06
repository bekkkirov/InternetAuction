import {Component, OnInit} from '@angular/core';
import {User} from "../../models/user.model";
import {UserService} from "../../services/user.service";
import {ActivatedRoute} from "@angular/router";
import {LotService} from "../../services/lot.service";
import {ToastrService} from "ngx-toastr";
import {LoggedInUser} from "../../models/logged-in-user.model";
import {AccountService} from "../../services/account.service";
import {take} from "rxjs";

@Component({
    selector: 'app-user-detail',
    templateUrl: './user-detail.component.html',
    styleUrls: ['./user-detail.component.css']
})
export class UserDetailComponent implements OnInit {
    currentUser:  LoggedInUser;
    user: User;
    pageNumber: number = 1;
    tab: string = "regLots"

    constructor(private userService: UserService, private accountService: AccountService,
                private lotService: LotService, private route: ActivatedRoute,
                private toastr: ToastrService) {
    }

    ngOnInit(): void {
        this.accountService.currentUser$.pipe(take(1)).subscribe(result => this.currentUser = result);
        this.loadUser();
    }

    changePage(event) {
        this.pageNumber = event;
    }

    changeTab(tab: string) {
        this.pageNumber = 1;
        this.tab = tab;
    }

    deleteLot(lotId: number) {
        this.lotService.deleteLot(lotId).pipe(take(1)).subscribe( {
            next: () => this.loadUser(),
            complete: () => this.toastr.success("Lot is successfully deleted")
        });
    }

    loadUser() {
        this.userService.getByUserName(this.route.snapshot.paramMap.get('userName'))
                        .pipe(take(1))
                        .subscribe(result => this.user = result);
    }
}
