import {Component, OnInit} from '@angular/core';
import {Lot} from "../../models/lot.model";
import {ActivatedRoute, Router} from "@angular/router";
import {LotService} from "../../services/lot.service";
import {FormControl, FormGroup, Validators} from "@angular/forms";
import * as moment from 'moment';
import {LoggedInUser} from "../../models/logged-in-user.model";
import {AccountService} from "../../services/account.service";
import {take} from "rxjs";
import {ToastrService} from "ngx-toastr";

@Component({
    selector: 'app-lot-detail',
    templateUrl: './lot-detail.component.html',
    styleUrls: ['./lot-detail.component.css']
})
export class LotDetailComponent implements OnInit {
    lot: Lot;
    moment = moment;
    currentUser: LoggedInUser;
    canBid: boolean;

    form = new FormGroup({
        "bidValue": new FormControl(null, [Validators.required, Validators.min(1)])
    });

    constructor(private lotService: LotService, private route: ActivatedRoute,
                private accountService: AccountService, private toastr: ToastrService) {
    }

    ngOnInit(): void {
        this.accountService.currentUser$.pipe(take(1)).subscribe(user => this.currentUser = user);
        this.loadLot();
    }

    saleEnded() {
        return moment(this.lot.saleEndTime).isBefore(new Date());
    }

    placeBid() {
        this.lotService.placeBid(this.lot.id, this.form.value).subscribe({
            complete: () => {
                this.loadLot();
                this.toastr.success("Bid has been successfully created")
            }
        });
    }

    loadLot() {
        this.lotService.getLot(+this.route.snapshot.paramMap.get('lotId')).subscribe(result => {
            this.lot = result;
            this.form.patchValue({"bidValue": +(result.currentPrice + 5).toFixed(2)});
            this.canBid = this.currentUser.userName !== this.lot.sellerUserName && !this.saleEnded();
        });
    }
}
