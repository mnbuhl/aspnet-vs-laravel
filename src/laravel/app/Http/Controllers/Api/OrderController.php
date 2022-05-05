<?php

namespace App\Http\Controllers\Api;

use App\Helpers\OrdersQuery;
use App\Http\Controllers\Controller;
use App\Http\Requests\Orders\GetOrdersRequest;
use App\Http\Requests\Orders\StoreOrderRequest;
use App\Models\Address;
use App\Models\Order;
use App\Models\OrderLine;
use App\Models\ShippingDetails;
use DB;
use Illuminate\Http\JsonResponse;
use Illuminate\Support\Facades\Log;

class OrderController extends Controller
{
    public function index(GetOrdersRequest $request): JsonResponse
    {
        $params = new OrdersQuery($request->validated());

        $orders = Order::where(function ($query) use ($params) {
            if (isset($params->userId)) {
                $query->where('user_id', $params->userId);
            }
        })
            ->with($params->with)
            ->orderBy($params->sort, $params->sortDirection)
            ->simplePaginate($params->pageSize, ['*'], 'pageIndex', $params->pageIndex);

        return response()->json($orders);
    }

    /**
     * @throws \Throwable
     */
    public function store(StoreOrderRequest $request): JsonResponse
    {
        try {
            DB::beginTransaction();

            $order = Order::make([
                'id' => $request->validated('id'),
                'user_id' => $request->validated('user_id'),
                'date' => $request->validated('date'),
            ]);

            $shippingAddress = Address::create($request->validated('shipping_details.shipping_address'));
            $shippingDetails = ShippingDetails::create([
                ...$request->validated('shipping_details'),
                'shipping_address_id' => $shippingAddress->id
            ]);

            $order->billingAddress()->associate(Address::create($request->validated('billing_address')));
            $order->shippingDetails()->associate($shippingDetails);

            $orderLines = $order->orderLines()->makeMany($request->validated('order_lines'));
            $order->calculateTotal($orderLines)->save();

            $order->save();
            $order->orderLines()->saveMany($orderLines);

            $orderLines->each(function (OrderLine $orderLine) {
                $orderLine->load('product')->product->updateQuantity($orderLine->quantity)->save();
            });
        } catch (\Exception $e) {
            DB::rollBack();
            Log::error($e->getMessage());
            return response()->json(['message' => 'Failed to create order'], 400);
        }

        DB::commit();

        return response()->json($order, 201);
    }

    public function show(Order $order): JsonResponse
    {
        $order->load('billingAddress', 'shippingDetails', 'orderLines', 'user', 'orderLines.product');
        return response()->json($order);
    }

    public function destroy(Order $order): JsonResponse
    {
        $order->load('shippingDetails', 'orderLines', 'orderLines.product');

        if (isset($order->shippingDetails->shipped_at)) {
            return response()->json(['message' => 'Cannot delete shipped order'], 400);
        }

        $order->orderLines->each(function (OrderLine $orderLine) {
            $orderLine->product->updateQuantity(-$orderLine->quantity)->save();
        });

        $order->delete();

        return response()->json(null, 204);
    }
}
