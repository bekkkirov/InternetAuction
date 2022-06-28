import {Component, OnInit} from '@angular/core';
import {LotService} from "../../services/lot.service";
import {LotCategory} from "../../models/lot-category.model";
import {take} from "rxjs";
import {LotPreview} from "../../models/lot-preview-model";
import {ActivatedRoute, Router} from "@angular/router";
import {LotParameters} from "../../models/lot-parameters.model";
import {FormControl, FormGroup, Validators} from "@angular/forms";
import * as moment from 'moment';
import {PaginatedResult} from "../../models/paginated-result.model";
import {LotsListMode} from "../../models/lots-list-mode";

@Component({
    selector: 'app-lots-list',
    templateUrl: './lots-list.component.html',
    styleUrls: ['./lots-list.component.css']
})
export class LotsListComponent implements OnInit {
    categories: LotCategory[];
    lots: PaginatedResult<LotPreview[]> = new PaginatedResult<LotPreview[]>();
    lotParams: LotParameters = new LotParameters();
    mode: LotsListMode = LotsListMode.BaseMode;
    searchValue: string;
    categoryId: number = 1;
    moment = moment;

    form: FormGroup = new FormGroup({
        "minPrice": new FormControl(0, [Validators.required, Validators.min(0)]),
        "maxPrice": new FormControl("", [Validators.min(1)]),
        "order": new FormControl("PriceAscending"),
    })

    constructor(private lotService: LotService, private router: Router,
                private route: ActivatedRoute) {
        if(this.router.url.includes("categories") ) {
            this.categoryId = +this.route.snapshot.paramMap.get('categoryId');
            this.mode = LotsListMode.CategoryMode;
        }

        else if(this.route.snapshot.paramMap.get('searchValue')) {
            this.mode = LotsListMode.SearchMode;
        }

        this.get();
    }

    ngOnInit(): void {
        this.route.params.subscribe(result => {
            this.searchValue = result['searchValue']
            this.get()
        })
        this.getCategories();
    }

    get() {
        switch (this.mode) {
            case LotsListMode.BaseMode:
                this.getLots()
                break;
            case LotsListMode.CategoryMode:
                this.getLotsByCategory(this.categoryId);
                break;
            case LotsListMode.SearchMode:
                this.searchForLots(this.searchValue)
                break;

        }
    }

    getCategories() {
        this.lotService.getCategories().pipe(take(1)).subscribe(response => this.categories = response);
    }

    getLots() {
        this.lotService.getLotPreviews(this.lotParams).pipe(take(1)).subscribe(response => {
            this.lots = response;
        });
    }

    getLotsByCategory(categoryId: number) {
        this.lotService.getLotsByCategory(categoryId, this.lotParams).pipe(take(1)).subscribe(response => {
            this.lots = response;
        });
    }

    searchForLots(searchValue: string) {
        this.lotService.search(searchValue, this.lotParams).pipe(take(1)).subscribe(response => {
            this.lots = response;
        });
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
