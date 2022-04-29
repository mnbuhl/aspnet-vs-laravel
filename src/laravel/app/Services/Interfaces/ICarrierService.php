<?php

namespace App\Services\Interfaces;

interface ICarrierService
{
    public function isOrderShipped(string $orderId);
    public function isOrderDelivered(string $orderId);
}
