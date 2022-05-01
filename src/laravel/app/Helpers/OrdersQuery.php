<?php

namespace App\Helpers;

class OrdersQuery extends DefaultQuery
{
    public ?string $userId;
    public array $with = ['billingAddress', 'user', 'shippingDetails', 'orderLines'];

    public function __construct(array $data)
    {
        parent::__construct($data);

        $this->userId = $data['userId'] ?? null;
    }
}
