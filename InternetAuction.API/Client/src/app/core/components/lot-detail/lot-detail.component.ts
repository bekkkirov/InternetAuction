import {Component, OnInit} from '@angular/core';
import {LotModel} from "../../models/lot.model";
import {ActivatedRoute, Router} from "@angular/router";
import {LotService} from "../../services/lot.service";
import {FormControl, FormGroup, Validators} from "@angular/forms";
import * as moment from 'moment';

@Component({
    selector: 'app-lot-detail',
    templateUrl: './lot-detail.component.html',
    styleUrls: ['./lot-detail.component.css']
})
export class LotDetailComponent implements OnInit {
    lot: LotModel;
    moment = moment;

    form = new FormGroup({
        "bidValue": new FormControl(null, [Validators.required])
    });

    constructor(private lotService: LotService, private route: ActivatedRoute) {
    }

    ngOnInit(): void {
        this.lotService.getLot(+this.route.snapshot.paramMap.get('lotId')).subscribe(result => {
            this.lot = result;
            this.form.patchValue({"bidValue": result.currentPrice + 5});
        });

    }
}
