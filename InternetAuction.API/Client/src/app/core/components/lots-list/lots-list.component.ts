import {Component, OnInit} from '@angular/core';
import {LotService} from "../../services/lot.service";
import {LotCategoryModel} from "../../models/lot-category.model";
import {take} from "rxjs";
import {LotPreviewModel} from "../../models/lot-preview-model";
import {PaginationModel} from "../../models/pagination.model";
import {Router} from "@angular/router";
import {LotParameters} from "../../models/lot-parameters.model";
import {FormControl, FormGroup, Validators} from "@angular/forms";
import * as moment from 'moment';

@Component({
    selector: 'app-lots-list',
    templateUrl: './lots-list.component.html',
    styleUrls: ['./lots-list.component.css']
})
export class LotsListComponent implements OnInit {
    categories: LotCategoryModel[];
    lots: LotPreviewModel[];
    pagination: PaginationModel;
    lotParams: LotParameters = new LotParameters();
    isCategorySelected: boolean;
    categoryId: number = 1;
    moment = moment;

    form: FormGroup = new FormGroup({
        "minPrice": new FormControl(0, [Validators.required, Validators.min(0)]),
        "maxPrice": new FormControl("", [Validators.min(1)]),
        "order": new FormControl("PriceAscending"),
    })

    constructor(private lotService: LotService, private router: Router) {
        if(this.router.url.includes("categories") ) {
            this.isCategorySelected = true;
            this.getLotsByCategory(this.categoryId);
        }

        else {
            this.isCategorySelected = false;
            this.getLots();
        }
    }

    ngOnInit(): void {
        this.getCategories();
    }

    get() {
        !this.isCategorySelected ? this.getLots() : this.getLotsByCategory(this.categoryId);
    }

    getCategories() {
        this.lotService.getCategories().pipe(take(1)).subscribe(response => this.categories = response);
    }

    getLots() {
        this.lotService.getLotPreviews(this.lotParams).pipe(take(1)).subscribe(response => {
            this.lots = response.result;
            this.pagination = response.pagination;
        });
    }

    getLotsByCategory(categoryId: number) {
        this.lotService.getLotsByCategory(categoryId, this.lotParams).pipe(take(1)).subscribe(response => {
            this.lots = response.result;
            this.pagination = response.pagination;
        })
    }

    changePage(event) {
        this.lotParams.pageNumber = event;
        this.get();
        window.scrollTo(0, 0);
    }

    selectCategory(categoryId: number) {
        this.lotParams.pageNumber = 1;
        this.categoryId = categoryId;
        this.getLotsByCategory(this.categoryId);
    }

    applyFilters() {
        this.lotParams.minPrice = this.form.get('minPrice')?.value;
        this.lotParams.maxPrice = this.form.get('maxPrice')?.value;
        this.lotParams.orderOptions = this.form.get('order')?.value;
        this.lotParams.pageNumber = 1;

        this.get();
    }
}
