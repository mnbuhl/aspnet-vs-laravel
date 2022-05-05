<?php

namespace App\Services\Interfaces;

interface ICarrierService
{
    public function isOrderShipped(string $orderId): bool;
    public function isOrderDelivered(string $orderId): bool;
}
