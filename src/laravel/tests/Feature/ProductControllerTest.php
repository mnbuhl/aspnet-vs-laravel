<?php

namespace Tests\Feature;

use Illuminate\Foundation\Testing\RefreshDatabase;
use Illuminate\Foundation\Testing\WithFaker;
use Illuminate\Http\Response;
use Illuminate\Pagination\Paginator;
use Tests\TestCase;

class ProductControllerTest extends TestCase
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
        $searchTerm = 'plc';

        $response = $this->get('/api/v1/products?search=' . $searchTerm);

        dd($response->json());
    }
}
