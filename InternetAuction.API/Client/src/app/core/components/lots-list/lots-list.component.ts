import {Component, OnInit} from '@angular/core';
import {LotService} from "../../services/lot.service";
import {LotCategoryModel} from "../../models/lot-category.model";
import {take} from "rxjs";
import {LotPreviewModel} from "../../models/lot-preview-model";
import {PaginationModel} from "../../models/pagination.model";

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

    constructor(private lotService: LotService) {
    }

    ngOnInit(): void {
        this.lotService.getCategories().pipe(take(1)).subscribe(response => this.categories = response);
        this.getLots();
    }

    getLots() {
        this.lotService.getLotPreviews(this.pageNumber).pipe(take(1)).subscribe(response => {
            this.lots = response.result;
            this.pagination = response.pagination;
        });
    }

    changePage(event) {
        this.pageNumber = event;
        this.getLots();
        window.scrollTo(0, 0);
    }

}
