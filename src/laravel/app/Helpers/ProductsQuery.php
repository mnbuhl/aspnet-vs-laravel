<?php

namespace App\Helpers;

class ProductsQuery
{
    public int $page;
    public int $pageSize;
    public string $sortBy;
    public string $sortDirection;
    public ?string $search;

    public function __construct(array $query)
    {
        $this->page = $query['page'] ?? 1;
        $this->pageSize = $query['pageSize'] ?? 10;
        $this->sortBy = $query['sortBy'] ?? 'created_at';
        $this->sortDirection = $query['sortDirection'] ?? 'desc';
        $this->search = $query['search'] ?? null;
    }
}
