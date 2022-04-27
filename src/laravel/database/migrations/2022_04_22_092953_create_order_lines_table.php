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
        Schema::create('order_lines', function (Blueprint $table) {
            $table->uuid('id')->primary();
            $table->bigInteger('price');
            $table->integer('quantity');
            $table->integer('discount');
            $table->foreignUuid('order_id')
                ->references('id')
                ->on('orders')
                ->cascadeOnDelete();
            $table->foreignUuid('product_id')
                ->nullable()
                ->references('id')
                ->on('products')
                ->nullOnDelete();
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
        Schema::dropIfExists('order_lines');
    }
};
