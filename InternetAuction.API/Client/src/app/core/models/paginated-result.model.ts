import {Pagination} from "./pagination.model";

export class PaginatedResult<T> {
    result: T;
    pagination: Pagination;

    constructor(result?: T, pagination?: Pagination) {
        this.result = result;
        this.pagination = pagination;
    }
}
