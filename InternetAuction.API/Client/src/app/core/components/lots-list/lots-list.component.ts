import {Component, OnInit} from '@angular/core';
import {LotService} from "../../services/lot.service";
import {LotCategoryModel} from "../../models/lot-category.model";
import {take} from "rxjs";
import {LotPreviewModel} from "../../models/lot-preview-model";

@Component({
    selector: 'app-lots-list',
    templateUrl: './lots-list.component.html',
    styleUrls: ['./lots-list.component.css']
})
export class LotsListComponent implements OnInit {
    categories: LotCategoryModel[];
    lots: LotPreviewModel[];

    constructor(private lotService: LotService) {
    }

    ngOnInit(): void {
        this.lotService.getCategories().pipe(take(1)).subscribe(result => this.categories = result);
        this.lotService.getLotPreviews().pipe(take(1)).subscribe(result => this.lots = result);
    }

}
