import {PaginationModel} from "./pagination.model";

export class PaginatedResultModel<T> {
    result: T;
    pagination: PaginationModel;
}
