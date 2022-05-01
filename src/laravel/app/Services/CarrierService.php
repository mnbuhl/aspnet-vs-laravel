<?php

namespace App\Services;

use App\Services\Interfaces\ICarrierService;

class CarrierService implements ICarrierService
{
    public function isOrderShipped(string $orderId): bool
    {
        return random_int(0, strlen($orderId)) < strlen($orderId) / 4;
    }

    public function isOrderDelivered(string $orderId): bool
    {
        return random_int(0, strlen($orderId)) < strlen($orderId) / 4;
    }
}
