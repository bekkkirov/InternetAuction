import {Component, OnInit} from '@angular/core';
import {User} from "../../models/user.model";
import {UserService} from "../../services/user.service";
import {ActivatedRoute} from "@angular/router";
import {LotService} from "../../services/lot.service";
import {ToastrService} from "ngx-toastr";

@Component({
    selector: 'app-user-detail',
    templateUrl: './user-detail.component.html',
    styleUrls: ['./user-detail.component.css']
})
export class UserDetailComponent implements OnInit {
    user: User;
    pageNumber: number = 1;
    tab: string = "regLots"

    constructor(private userService: UserService, private lotService: LotService,
                private route: ActivatedRoute, private toastr: ToastrService) {
    }

    ngOnInit(): void {
        this.loadUser();
    }

    changePage(event) {
        this.pageNumber = event;
    }

    changeTab(tab: string) {
        this.tab = tab;
    }

    deleteLot(lotId: number) {
        this.lotService.deleteLot(lotId).subscribe( {
            next: () => this.loadUser(),
            complete: () => this.toastr.success("Lot is successfully deleted")
        });
    }

    loadUser() {
        this.userService.getByUserName(this.route.snapshot.paramMap.get('userName'))
            .subscribe(result => this.user = result);
    }
}
