import {Component, OnInit, ViewChild} from '@angular/core';
import {LotService} from "../../services/lot.service";
import {LotCategoryModel} from "../../models/lot-category.model";
import {FormControl, FormGroup, Validators} from "@angular/forms";
import {AppValidators} from "../../validators/app-validators";
import * as moment from "moment";
import {LotModel} from "../../models/lot.model";
import {Router} from "@angular/router";

@Component({
    selector: 'app-lot-create',
    templateUrl: './lot-create.component.html',
    styleUrls: ['./lot-create.component.css']
})
export class LotCreateComponent implements OnInit {
    categories: LotCategoryModel[];
    moment = moment;
    @ViewChild('fileInput') fileInput;

    form: FormGroup = new FormGroup({
        "name": new FormControl(null, [Validators.required, AppValidators.validateLength(5, 30)]),
        "description": new FormControl(null, [Validators.required, AppValidators.validateLength(10, 250)]),
        "initialPrice": new FormControl(1, [Validators.required, Validators.min(1)]),
        "saleEndTime": new FormControl(null, [Validators.required, AppValidators.validateDate]),
        "quantity": new FormControl(1, [Validators.required, Validators.min(1)]),
        "categoryId": new FormControl("", [Validators.required])
    });

    constructor(private lotService: LotService, private router: Router) {
    }

    ngOnInit(): void {
        this.lotService.getCategories().subscribe(result => this.categories = result);
    }

    createLot() {
        this.lotService.addLot(this.form.value).subscribe({
            next: (result) => {
                let image = this.fileInput.nativeElement.files[0];

                if (image) {
                    this.lotService.addLotImage(image, result.id).subscribe();
                }
            },

            complete: () => this.router.navigateByUrl('/')
        });
    }
}
