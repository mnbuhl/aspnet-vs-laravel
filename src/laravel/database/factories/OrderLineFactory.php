<?php

namespace Database\Factories;

use App\Models\Product;
use Illuminate\Database\Eloquent\Factories\Factory;

/**
 * @extends Factory
 */
class OrderLineFactory extends Factory
{
    /**
     * Define the model's default state.
     *
     * @return array
     */
    public function definition(): array
    {
        $product = Product::all()->random()->first();

        return [
            'product_id' => $product->id,
            'quantity' => random_int(1, 10),
            'price' => $product->price,
            'discount' => random_int(0, 10) > 5 ? random_int(0, 100) : 0,
        ];
    }
}
