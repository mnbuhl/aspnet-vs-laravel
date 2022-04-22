<?php

namespace Database\Factories;

use Illuminate\Database\Eloquent\Factories\Factory;

/**
 * @extends Factory
 */
class ProductFactory extends Factory
{
    /**
     * Define the model's default state.
     *
     * @return array<string, mixed>
     */
    public function definition()
    {
        return [
            'name' => $this->faker->company,
            'price' => $this->faker->randomNumber(6),
            'description' => $this->faker->text,
            'amount_in_stock' => $this->faker->randomNumber(4),
        ];
    }
}
