<script lang="ts" setup>
import { Product, ProductFactory } from '../models/product';
import Score from './Score.vue';
import { ref } from 'vue'
import Agent from '../api/agent';
import { ResUseStopwatch, useStopwatch } from 'vue-timer-hook'
import { UserFactory } from '../models/user';

const timers: ResUseStopwatch[] = [];

for (let i = 0; i < 12; i++) {
    timers.push(useStopwatch(0, false));
}

const initiateBenchmark = async () => {
    await dotnetBenchmark();
    await laravelBenchmark();
}

function startTimer(number: number) {
    timers[number].start();
}

function stopTimer(number: number) {
    timers[number].pause();
}

const dotnetBenchmark = async () => {
    const agent = new Agent('asp.net');
    messages.value.push('Starting ASP.NET Benchmark...')

    const users = UserFactory.create(1000);
    const products = ProductFactory.create(4000);

    await agent.Demo.deleteDb();

    startTimer(0);
    startTimer(1);
    startTimer(2);

    // 0 .. 1.000 post requests
    for (let i = 0; i < users.length; i++) {
        await agent.Users.post(users[i]);
    }
    stopTimer(0);

    // 1.000 .. 5.000 post requests
    for (let i = 0; i < products.length; i++) {
        await agent.Products.post(products[i]);
    }
    stopTimer(1);

    stopTimer(2);

    messages.value.push('Finished ASP.NET Benchmark')
}

const laravelBenchmark = async () => {
    const agent = new Agent('laravel');
    messages.value.push('Starting Laravel Benchmark...')

    startTimer(6);
    messages.value.push('Finished Laravel Benchmark')
}

const messages = ref(['']);
</script>

<template>
    <div class="text-center mx-auto my-4">
        <h1>ASP.NET vs Laravel Benchmarks</h1>
    </div>
    <div class="grid grid-cols-2 text-center">
        <Score :timers="timers.slice(0, 6)" :framework="'ASP.NET Core 6.0'" />
        <Score :timers="timers.slice(6, 12)" :framework="'Laravel 9'" />
    </div>
    <div class="text-center mx-auto">
        <button @click="initiateBenchmark">Benchmark</button>
    </div>
    <div class="w-72 mx-auto">
        <h2 class="text-center">Output</h2>
        <p v-for="message in messages">{{ message }}</p>
    </div>
</template>

