<?php

namespace App\Helpers;

class DefaultQuery
{
    public int $pageIndex;
    public int $pageSize;
    public string $sort;
    public string $sortDirection;

    protected function __construct(array $query)
    {
        $this->pageIndex = $query['pageIndex'] ?? 1;
        $this->pageSize = $query['pageSize'] ?? 6;
        $this->sortDirection = isset($query['sort']) ? $this->getSortDirection($query['sort']) : 'desc';
        $this->sort = isset($query['sort']) ? $this->getSortString($query['sort']) : 'created_at';
    }

    protected function getSortDirection(string $sort): string
    {
        switch ($sort) {
            case $sort[0] !== '-':
                return 'asc';
            default:
                return 'desc';
        }
    }

    protected function getSortString(string $sort): string
    {
        if ($sort[0] === '-') {
            return substr($sort, 1);
        }

        return $sort;
    }
}
