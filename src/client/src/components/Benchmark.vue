<script lang="ts" setup>
import agent from '../api/agent'
import { ProductFactory } from '../models/product';
import Score from './Score.vue';

const producs = ProductFactory.create(500);

console.log(producs);

const order = await agent.Orders.get("0303d232-6c1d-4c23-88db-849663ae77e6");
defineExpose({
    order
})

console.log(order);
</script>

<template>
    <div class="text-center mx-auto my-4">
        <h1>ASP.NET vs Laravel Benchmarks</h1>
    </div>
    <div class="grid grid-cols-2 text-center">
        <Score :framework="'ASP.NET Core 6.0'" />
        <Score :framework="'Laravel 9'" />
    </div>
    <div class="text-center mx-auto">
        <button>Benchmark</button>
    </div>
</template>

