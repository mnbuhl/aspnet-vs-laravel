<?php

namespace Database\Seeders;

use App\Models\Address;
use App\Models\Order;
use App\Models\OrderLine;
use App\Models\Product;
use App\Models\ShippingDetails;
use App\Models\User;
use Illuminate\Database\Console\Seeds\WithoutModelEvents;
use Illuminate\Database\Seeder;

class DatabaseSeeder extends Seeder
{
    /**
     * Seed the application's database.
     *
     * @return void
     */
    public function run()
    {
        $testing = config('app.env') === 'testing';

        Product::factory()->count($testing ? 20 : 150)->create();
        $users = User::factory()->count($testing ? 10 : 100)->create();

        $users->each(function (User $user) use ($testing) {
            $orderCount = random_int(0, $testing ? 5 : 20);

            for ($i = 0; $i < $orderCount; $i++) {
                $user->orders()->save(Order::factory()->make([
                    'shipping_details_id' => ShippingDetails::factory()->create([
                        'shipping_address_id' => Address::factory()->create()->id
                    ])->id,
                    'billing_address_id' => Address::factory()->create()->id,
                ]));
            }
        });

        Order::all()->each(function (Order $order) {
           $order->orderLines()->saveMany(OrderLine::factory()->count(random_int(1, 5))->make());
           $order->calculateTotal($order->load('orderLines')->orderLines)->save();
        });
    }
}
