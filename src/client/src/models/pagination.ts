export interface Pagination {
    currentPage: number;
    itemsPerPage: number;
    totalItems: number;
    totalPages: number;
}

export class PaginatedResult<T> {
    public data: T;
    public pagination: Pagination;

    constructor(data: T, pagination: Pagination) {
        this.data = data;
        this.pagination = pagination;
    }
}

export class PaginationParams {
    public pageIndex: number;
    public pageSize: number;

    constructor(pageIndex = 1, pageSize = 6) {
        this.pageIndex = pageIndex,
        this.pageSize = pageSize;
    }
}