<?php

namespace App\Http\Controllers\Api;

use App\Helpers\ProductsQuery;
use App\Http\Controllers\Controller;
use App\Http\Requests\GetProductsRequest;
use App\Http\Requests\StoreProductRequest;
use App\Http\Requests\UpdateProductRequest;
use App\Models\Product;
use Illuminate\Http\JsonResponse;
use Illuminate\Support\Facades\Log;

class ProductController extends Controller
{
    public function index(GetProductsRequest $request): JsonResponse
    {
        $params = new ProductsQuery($request->validated());

        $products = Product::where([
            [function ($query) use ($params) {
                if (isset($params->search)) {
                    $query->where('name', 'ILIKE', "%$params->search%")
                        ->orWhere('description', 'ILIKE', "%$params->search%");
                }
            }]
        ])
            ->orderBy($params->sort, $params->sortDirection)
            ->simplePaginate($params->pageSize, ['*'], 'page', $params->pageIndex);

        return response()->json($products);
    }

    public function store(StoreProductRequest $request): JsonResponse
    {
        $product = Product::create($request->validated());

        if (!isset($product)) {
            Log::error('Failed to create product');
            return response()->json(['message' => 'Failed to create product'], 400);
        }

        return response()->json($product, 201);
    }

    public function show(Product $product): JsonResponse
    {
        return response()->json($product);
    }

    public function update(UpdateProductRequest $request, Product $product): JsonResponse
    {
        try {
            $product->updateOrFail($request->validated());
        } catch (\Throwable $e) {
            Log::error("Failed to update product", $e->getTrace());
            return response()->json(['message' => 'Product not updated'], 400);
        }

        return response()->json(null, 204);
    }

    public function destroy(Product $product): JsonResponse
    {
        try {
            $product->deleteOrFail();
        } catch (\Throwable $e) {
            Log::error("Failed to delete product", $e->getTrace());
            return response()->json(['message' => 'Product not deleted'], 400);
        }

        return response()->json(null, 204);
    }
}
