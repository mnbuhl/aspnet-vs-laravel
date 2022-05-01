<?php

namespace App\Jobs;

use App\Models\Order;
use App\Services\Interfaces\ICarrierService;
use Illuminate\Bus\Queueable;
use Illuminate\Contracts\Queue\ShouldBeUnique;
use Illuminate\Contracts\Queue\ShouldQueue;
use Illuminate\Foundation\Bus\Dispatchable;
use Illuminate\Queue\InteractsWithQueue;
use Illuminate\Queue\SerializesModels;

class CheckShippingStatus implements ShouldQueue
{
    use Dispatchable, InteractsWithQueue, Queueable, SerializesModels;

    private ICarrierService $carrierService;

    /**
     * Create a new job instance.
     *
     * @return void
     */
    public function __construct()
    {
        $this->carrierService = app()->make(ICarrierService::class);
    }

    /**
     * Execute the job.
     *
     * @return void
     */
    public function handle(): void
    {
        $ordersShipped = 0;
        $ordersDelivered = 0;

        $notShippedOrders = Order::with('shippingDetails')
            ->whereRelation('shippingDetails', 'shipped_at', '=', null)
            ->get();

        $notDeliveredOrders = Order::with('shippingDetails')
            ->whereRelation('shippingDetails', 'shipped_at', '!=', null)
            ->whereRelation('shippingDetails', 'shipped_at', '<', now()->addDay())
            ->whereRelation('shippingDetails', 'delivered_at', '=', null)
            ->get();

        $notShippedOrders->each(function (Order $order) use (&$ordersShipped) {
            if (!$this->carrierService->isOrderShipped($order)) {
                return;
            }

            $order->shippingDetails->shipped_at = now();
            $order->shippingDetails->save();
            $ordersShipped++;
        });

        $notDeliveredOrders->each(function (Order $order) use (&$ordersDelivered) {
            if (!$this->carrierService->isOrderDelivered($order)) {
                return;
            }

            $order->shippingDetails->delivered_at = now();
            $order->shippingDetails->save();
            $ordersDelivered++;
        });

        \Log::info("Orders shipped: {$ordersShipped}");
        \Log::info("Orders delivered: {$ordersDelivered}");
    }
}
