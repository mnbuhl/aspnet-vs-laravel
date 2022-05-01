<?php

namespace App\Helpers;

class ProductsQuery extends DefaultQuery
{
    public ?string $search;

    public function __construct(array $query)
    {
        parent::__construct($query);
        $this->search = $query['search'] ?? null;
    }
}
