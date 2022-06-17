import {Component, OnInit} from '@angular/core';
import {LotService} from "../../services/lot.service";
import {LotCategoryModel} from "../../models/lot-category.model";
import {take} from "rxjs";
import {LotPreviewModel} from "../../models/lot-preview-model";
import {PaginationModel} from "../../models/pagination.model";
import {Router} from "@angular/router";

@Component({
    selector: 'app-lots-list',
    templateUrl: './lots-list.component.html',
    styleUrls: ['./lots-list.component.css']
})
export class LotsListComponent implements OnInit {
    categories: LotCategoryModel[];
    lots: LotPreviewModel[];
    pagination: PaginationModel;
    pageNumber: number = 1;
    isCategorySelected = false;
    categoryId: number = 1;

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

    getCategories() {
        this.lotService.getCategories().pipe(take(1)).subscribe(response => this.categories = response);
    }

    getLots() {
        this.lotService.getLotPreviews(this.pageNumber).pipe(take(1)).subscribe(response => {
            this.lots = response.result;
            this.pagination = response.pagination;
        });
    }

    getLotsByCategory(categoryId: number) {
        this.lotService.getLotsByCategory(categoryId, this.pageNumber).pipe(take(1)).subscribe(response => {
            this.lots = response.result;
            this.pagination = response.pagination;
        })
    }

    changePage(event) {
        this.pageNumber = event;
        !this.isCategorySelected ? this.getLots() : this.getLotsByCategory(this.categoryId);
        window.scrollTo(0, 0);
    }

    selectCategory(categoryId: number) {
        this.pageNumber = 1;
        this.categoryId = categoryId;
        this.getLotsByCategory(this.categoryId);
    }
}
