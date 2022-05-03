<?php

namespace Tests\Feature;

use App\Models\Product;
use Symfony\Component\HttpFoundation\Response;
use Illuminate\Pagination\Paginator;
use Ramsey\Uuid\Uuid;
use Tests\TestCase;

class ProductTest extends TestCase
{
    /** @test */
    public function get_products_returns_200_ok(): void
    {
        $response = $this->get('/api/v1/products');

        $response->assertStatus(Response::HTTP_OK);
        $this->assertInstanceOf(Paginator::class, $response->original);
    }

    /** @test */
    public function get_products_with_page_size_20_returns_20_products(): void
    {
        $response = $this->get('/api/v1/products?pageSize=20');

        $response->assertStatus(Response::HTTP_OK);
        $this->assertCount(20, $response->json('data'));
        $this->assertInstanceOf(Paginator::class, $response->original);
    }

    /** @test */
    public function get_products_with_page_index_2_should_not_be_same_as_page_index_1(): void
    {
        $response1 = $this->get('/api/v1/products?pageIndex=1')->json();
        $response2 = $this->get('/api/v1/products?pageIndex=2')->json();

        $this->assertGreaterThan(0, $response1['data']);
        $this->assertGreaterThan(0, $response2['data']);

        $this->assertNotEquals($response1, $response2);
    }

    /** @test */
    public function get_products_with_search_param_should_only_return_matching_products(): void
    {
        $this->post('api/v1/products', [
            'name' => 'Test PLC',
            'description' => 'Test Description',
            'price' => 1000,
            'amount_in_stock' => 10,
        ]);

        $searchTerm = 'plc';

        $response = $this->get('/api/v1/products?search=' . $searchTerm);

        $this->assertGreaterThan(0, $response->json('data'));

        foreach ($response->json('data') as $product) {
            $this->assertStringContainsStringIgnoringCase($searchTerm, $product['name']);
        }
    }

    /** @test */
    public function get_product_with_existing_id_returns_200_ok(): void
    {
        $product = Product::first();

        $response = $this->get('/api/v1/products/' . $product->id);

        $response->assertStatus(Response::HTTP_OK);
        $this->assertInstanceOf(Product::class, $response->original);
    }

    /** @test */
    public function get_product_with_non_existing_id_returns_404_not_found(): void
    {
        $response = $this->get('/api/v1/products/' . Uuid::uuid4());

        $response->assertStatus(Response::HTTP_NOT_FOUND);
        $this->assertEquals('Resource not found.', $response->json('message'));
    }

    /** @test */
    public function create_product_with_valid_product_returns_201_created(): void
    {
        $product = Product::make([
            'name' => 'Test PLC',
            'description' => 'Test Description',
            'price' => 1000,
            'amount_in_stock' => 10
        ]);

        $response = $this->post('/api/v1/products', $product->toArray());

        $response->assertStatus(Response::HTTP_CREATED);
        $this->assertEquals($product->name, $response->json('name'));
        $this->assertEquals($product->description, $response->json('description'));
        $this->assertEquals($product->price, $response->json('price'));
        $this->assertEquals($product->amount_in_stock, $response->json('amount_in_stock'));
        $this->assertInstanceOf(Product::class, $response->original);
    }

    /** @test */
    public function create_product_with_invalid_product_returns_400_bad_request (): void
    {
        $product = Product::make([
            'name' => 'Test PLC',
            'description' => 'Test Description',
        ]);

        $response = $this->post('/api/v1/products', $product->toArray());

        $response->assertStatus(Response::HTTP_BAD_REQUEST);
        $this->assertIsString($response->json('message'));
    }

    /** @test */
    public function update_product_with_valid_params_returns_204_no_content(): void
    {
        $product = Product::first();

        $updateProduct = Product::make([
            'name' => 'Test Updated PLC',
            'description' => 'Test Updated Description',
            'price' => 2000,
            'amount_in_stock' => 20
        ]);

        $response = $this->put('/api/v1/products/' . $product->id, $updateProduct->toArray());

        $product = $product->refresh();

        $response->assertStatus(Response::HTTP_NO_CONTENT);
        $this->assertEquals($product->name, $updateProduct->name);
        $this->assertEquals($product->description, $updateProduct->description);
        $this->assertEquals($product->price, $updateProduct->price);
        $this->assertEquals($product->amount_in_stock, $updateProduct->amount_in_stock);
    }

    /** @test */
    public function delete_product_with_existing_id_returns_204_no_content(): void
    {
        $product = Product::first();

        $response = $this->delete('/api/v1/products/' . $product->id);

        $deletedProduct = Product::find($product->id);

        $response->assertStatus(Response::HTTP_NO_CONTENT);
        $this->assertNull($deletedProduct);
    }

    /** @test */
    public function delete_product_with_non_existing_id_returns_404_not_found(): void
    {
        $response = $this->delete('/api/v1/products/' . Uuid::uuid4());
        $response->assertStatus(Response::HTTP_NOT_FOUND);
    }
}
