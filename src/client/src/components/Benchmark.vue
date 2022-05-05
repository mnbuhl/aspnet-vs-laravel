<template>
    <div class="text-center mx-auto my-4">
        <h1>ASP.NET vs Laravel Benchmarks</h1>
    </div>
    <div class="grid grid-cols-3 text-center">
        <Score :timers="timers.slice(0, 6)" :framework="'ASP.NET Core 6.0'" />
        <div class="w-60 text-left mx-auto">
            <div>
                <p class="inline-block float-left">ASP.NET total time: </p>
                <p class="inline-block float-right">{{ toTimeString(
                        timers[12].minutes.value, timers[12].seconds.value)
                }}</p>
            </div>
            <div>
                <p class="inline-block float-left">ASP.NET Post requests:</p>
                <p class="inline-block float-right">{{ requests[0] }}</p>
            </div>
            <div>
                <p class="inline-block float-left">ASP.NET Get requests:</p>
                <p class="inline-block float-right">{{ requests[1] }}</p>
            </div>
            <div>
                <p class="inline-block float-left">Laravel total time: </p>
                <p class="inline-block float-right">{{ toTimeString(
                        timers[13].minutes.value, timers[13].seconds.value)
                }}</p>
            </div>
            <div>
                <p class="inline-block float-left">Laravel Post requests:</p>
                <p class="inline-block float-right">{{ requests[2] }}</p>
            </div>
            <div>
                <p class="inline-block float-left">Laravel Get requests:</p>
                <p class="inline-block float-right">{{ requests[3] }}</p>
            </div>
        </div>
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


<script lang="ts" setup>
import { ProductFactory } from '../models/product';
import Score from './Score.vue';
import { ref } from 'vue'
import Agent from '../api/agent';
import { ResUseStopwatch, useStopwatch } from 'vue-timer-hook'
import { UserFactory } from '../models/user';
import { OrderFactory } from '../models/order';
import { toTimeString } from '../util';

const timers: ResUseStopwatch[] = [];
const messages = ref(['']);
const requests = ref([0, 0, 0, 0]);

for (let i = 0; i < 14; i++) {
    timers.push(useStopwatch(0, false));
}

const initiateBenchmark = async () => {
    // await dotnetBenchmark();
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

    messages.value.push('Cleaning up database...')

    const users = UserFactory.create(1000);
    const products = ProductFactory.create(4000);
    const orders = OrderFactory.create(5000, users, products);

    await agent.Demo.deleteDb();

    messages.value.push('Starting ASP.NET Benchmark...')

    startTimer(0);
    startTimer(1);
    startTimer(2);
    startTimer(12);

    // 0 .. 1.000 post requests
    for (let i = 0; i < users.length; i++) {
        await agent.Users.post(users[i]);
        requests.value[0]++;
    }

    stopTimer(0);

    // 1.000 .. 5.000 post requests
    for (let i = 0; i < products.length; i++) {
        await agent.Products.post(products[i]);
        requests.value[0]++;
    }

    stopTimer(1);

    // 5.000 .. 10.000 post requests
    for (let i = 0; i < orders.length; i++) {
        await agent.Orders.post(orders[i]);
        requests.value[0]++;
    }

    stopTimer(2);

    startTimer(3);
    startTimer(4);
    startTimer(5);

    // 0 .. 1.000 get requests
    for (let i = 0; i < users.length; i++) {
        await agent.Users.get(users[i].id);
        requests.value[1]++
    }

    stopTimer(3);

    // 1.000 .. 5.000 get requests
    for (let i = 0; i < products.length; i++) {
        await agent.Products.get(products[i].id);
        requests.value[1]++
    }

    stopTimer(4);

    // 5.000 .. 10.000 get requests
    for (let i = 0; i < orders.length; i++) {
        await agent.Orders.get(orders[i].id);
        requests.value[1]++
    }

    stopTimer(5);
    stopTimer(12);

    messages.value.push('Finished ASP.NET Benchmark')
}

const laravelBenchmark = async () => {
    const agent = new Agent('laravel');

    messages.value.push('Cleaning up database...')

    const users = UserFactory.create(10);
    const products = ProductFactory.create(10);
    const orders = OrderFactory.create(5000, users, products);

    await agent.Demo.deleteDb();

    messages.value.push('Starting Laravel Benchmark...')

    startTimer(6);
    startTimer(7);
    startTimer(8);
    startTimer(13);

    // 0 .. 1.000 post requests
    for (let i = 0; i < users.length; i++) {
        await agent.Users.post(users[i]);
        requests.value[2]++;
    }

    stopTimer(6);

    // 1.000 .. 5.000 post requests
    for (let i = 0; i < products.length; i++) {
        await agent.Products.post(products[i]);
        requests.value[2]++;
    }

    stopTimer(7);

    // 5.000 .. 10.000 post requests
    for (let i = 0; i < orders.length; i++) {
        await agent.Orders.post(orders[i]);
        requests.value[2]++;
    }

    stopTimer(8);

    startTimer(9);
    startTimer(10);
    startTimer(11);

    // 0 .. 1.000 get requests
    for (let i = 0; i < users.length; i++) {
        await agent.Users.get(users[i].id);
        requests.value[3]++
    }

    stopTimer(9);

    // 1.000 .. 5.000 get requests
    for (let i = 0; i < products.length; i++) {
        await agent.Products.get(products[i].id);
        requests.value[3]++
    }

    stopTimer(10);

    // 5.000 .. 10.000 get requests
    for (let i = 0; i < orders.length; i++) {
        await agent.Orders.get(orders[i].id);
        requests.value[3]++
    }

    stopTimer(11);

    stopTimer(13);
    messages.value.push('Finished Laravel Benchmark')
}
</script>
