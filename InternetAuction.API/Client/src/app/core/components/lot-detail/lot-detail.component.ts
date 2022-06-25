import {Component, OnInit} from '@angular/core';
import {Lot} from "../../models/lot.model";
import {ActivatedRoute, Router} from "@angular/router";
import {LotService} from "../../services/lot.service";
import {FormControl, FormGroup, Validators} from "@angular/forms";
import * as moment from 'moment';
import {LoggedInUser} from "../../models/logged-in-user.model";
import {AccountService} from "../../services/account.service";
import {take} from "rxjs";

@Component({
    selector: 'app-lot-detail',
    templateUrl: './lot-detail.component.html',
    styleUrls: ['./lot-detail.component.css']
})
export class LotDetailComponent implements OnInit {
    lot: Lot;
    moment = moment;
    currentUser: LoggedInUser;

    form = new FormGroup({
        "bidValue": new FormControl(null, [Validators.required])
    });

    constructor(private lotService: LotService, private route: ActivatedRoute, private accountService: AccountService) {
    }

    ngOnInit(): void {
        this.lotService.getLot(+this.route.snapshot.paramMap.get('lotId')).subscribe(result => {
            this.lot = result;
            this.form.patchValue({"bidValue": result.currentPrice + 5});
        });

        this.accountService.currentUser$.pipe(take(1)).subscribe(user => this.currentUser = user);
    }
}
