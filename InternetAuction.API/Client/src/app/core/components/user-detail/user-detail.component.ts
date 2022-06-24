import {Component, OnInit} from '@angular/core';
import {User} from "../../models/user.model";
import {UserService} from "../../services/user.service";
import {ActivatedRoute} from "@angular/router";

@Component({
    selector: 'app-user-detail',
    templateUrl: './user-detail.component.html',
    styleUrls: ['./user-detail.component.css']
})
export class UserDetailComponent implements OnInit {
    user: User;
    pageNumber: number = 1;
    tab: string = "regLots"

    constructor(private userService: UserService, private route: ActivatedRoute) {
    }

    ngOnInit(): void {
        this.userService.getByUserName(this.route.snapshot.paramMap.get('userName'))
                        .subscribe(result => this.user = result);
    }

    changePage(event) {
        this.pageNumber = event;
    }

    changeTab(tab: string) {
        this.tab = tab;
    }
}
