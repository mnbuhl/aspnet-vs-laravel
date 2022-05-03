<?php

namespace Tests\Feature;

use App\Models\Order;
use App\Models\Product;
use App\Models\User;
use Illuminate\Pagination\Paginator;
use Ramsey\Uuid\Uuid;
use Symfony\Component\HttpFoundation\Response;
use Tests\TestCase;

class OrderTest extends TestCase
{
    /** @test */
    public function get_orders_with_no_parameters_returns_200_ok(): void
    {
        $response = $this->get('/api/v1/orders');

        $response->assertStatus(Response::HTTP_OK);
        $this->assertIsArray($response->json('data'));
        $this->assertInstanceOf(Paginator::class, $response->original);
    }

    /** @test */
    public function get_orders_with_user_id_parameter_returns_orders_for_user(): void
    {
        $user = User::first();

        $response = $this->get('/api/v1/orders?userId='.$user->id);

        $response->assertStatus(Response::HTTP_OK);
        $this->assertIsArray($response->json('data'));
        $this->assertGreaterThan(0, $response->json('data'));

        foreach ($response->json('data') as $order) {
            $this->assertEquals($user->id, $order['user_id']);
        }
    }

    /** @test */
    public function get_order_with_valid_id_returns_order_with_relations(): void
    {
        $order = Order::first();

        $response = $this->get('/api/v1/orders/'.$order->id);

        $response->assertStatus(Response::HTTP_OK);
        $this->assertInstanceOf(Order::class, $response->original);
        $this->assertNotNull($response->json('user'));
        $this->assertNotNull($response->json('order_lines'));
        $this->assertNotNull($response->json('shipping_details'));
        $this->assertNotNull($response->json('billing_address'));
    }

    /** @test */
    public function get_order_with_invalid_id_returns_404_not_found(): void
    {
        $response = $this->get('/api/v1/orders/' . Uuid::uuid4());

        $response->assertStatus(Response::HTTP_NOT_FOUND);
    }

    /** @test */
    public function create_order_with_valid_params_returns_201_created(): void
    {
        $user = User::first();
        $products = Product::all();

        $response = $this->post('/api/v1/orders', [
            'user_id' => $user->id,
            'date' => now()->subDay(),
            'order_lines' => [
                [
                    'product_id' => $products[0]->id,
                    'price' => $products[0]->price,
                    'discount' => 0,
                    'quantity' => 1,
                ],
                [
                    'product_id' => $products[1]->id,
                    'price' => $products[1]->price,
                    'discount' => 0,
                    'quantity' => 2,
                ],
            ],
            'shipping_details' => [
                'shipping_address' => [
                    'city' => 'Anytown',
                    'address_line' => 'Test Rd 2',
                    'zip_code' => '12345',
                    'country' => 'Denmark',
                ],
                'carrier' => 'Test Carrier',
                'shipped_at' => now(),
            ],
            'billing_address' => [
                'city' => 'Anytown',
                'address_line' => 'Test Rd 1',
                'zip_code' => '12345',
                'country' => 'Denmark',
            ],
        ]);

        $response->assertStatus(Response::HTTP_CREATED);
        $this->assertInstanceOf(Order::class, $response->original);
    }

    /** @test */
    public function create_order_with_invalid_properties_returns_400_bad_request(): void
    {
        $this->post('api/v1/orders', Order::make()->toArray())
            ->assertStatus(Response::HTTP_BAD_REQUEST);
    }

    /** @test */
    public function delete_order_with_no_shipped_at_returns_204_no_content(): void
    {
        $order = Order::first();
        $order->shippingDetails->shipped_at = null;
        $order->shippingDetails->save();

        $response = $this->delete('/api/v1/orders/'.$order->id);

        $response->assertStatus(Response::HTTP_NO_CONTENT);
    }

    public function delete_order_with_shipped_at_returns_400_bad_request(): void
    {
        $order = Order::first();
        $order->shippingDetails->shipped_at = now();
        $order->shippingDetails->save();

        $response = $this->delete('/api/v1/orders/'.$order->id);

        $response->assertStatus(Response::HTTP_BAD_REQUEST);
    }
}
