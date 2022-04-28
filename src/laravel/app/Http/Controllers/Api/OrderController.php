<?php

namespace App\Http\Controllers\Api;

use App\Helpers\OrdersQuery;
use App\Http\Controllers\Controller;
use App\Http\Requests\Orders\GetOrdersRequest;
use App\Models\Order;
use Illuminate\Http\JsonResponse;
use Illuminate\Http\Request;

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
}
