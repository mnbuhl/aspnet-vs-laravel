<?php

namespace Database\Factories;

use App\Models\Address;
use Illuminate\Database\Eloquent\Factories\Factory;

/**
 * @extends Factory
 */
class ShippingDetailsFactory extends Factory
{
    /**
     * Define the model's default state.
     *
     * @return array
     */
    public function definition(): array
    {
        $shipped = random_int(0, 10) > 5;
        $delivered = $shipped && random_int(0, 10) > 3;

        return [
            'carrier' => $this->faker->randomElement(['Post Nord', 'DHL', 'UPS']),
            'shipped_at' => $shipped ? now()->subDays(3) : null,
            'delivered_at' => $delivered ? now() : null,
        ];
    }
}
