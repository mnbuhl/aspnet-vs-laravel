<?php

use Illuminate\Database\Migrations\Migration;
use Illuminate\Database\Schema\Blueprint;
use Illuminate\Support\Facades\Schema;

return new class extends Migration {
    /**
     * Run the migrations.
     *
     * @return void
     */
    public function up()
    {
        Schema::create('orders', function (Blueprint $table) {
            $table->uuid('id')->primary();
            $table->bigInteger('total')->default(0);
            $table->dateTime('date')->default(now());
            $table->foreignUuid('user_id')->nullable()
                ->references('id')
                ->on('users')
                ->nullOnDelete();
            $table->foreignUuid('billing_address_id')
                ->references('id')
                ->on('addresses')
                ->cascadeOnDelete();
            $table->foreignUuid('shipping_details_id')
                ->references('id')
                ->on('shipping_details')
                ->cascadeOnDelete();
            $table->timestamps();
        });
    }

    /**
     * Reverse the migrations.
     *
     * @return void
     */
    public function down()
    {
        Schema::dropIfExists('orders');
    }
};
