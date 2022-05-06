<template>
    <div class="text-center mx-auto my-4">
        <h1>ASP.NET vs Laravel Benchmarks</h1>
    </div>
    <div class="grid lg:w-[800px] w-full mx-auto lg:grid-cols-3 grid-cols-1 text-center mb-8">
        <div class="border rounded-lg w-64 mx-auto lg:mb-0 mb-4 order-1">
            <Score :timers="timers.slice(0, 6)" :framework="'ASP.NET Core 6.0'" />
        </div>
        <div class="w-64 border rounded-lg mx-auto p-6 lg:mb-0 mb-4 lg:order-2 order-first">
            <div class="text-left">
                <h4 class="text-center">ASP.NET Core 6.0</h4>
                <div class="flex flex-1 justify-between">
                    <p>Total time: </p>
                    <p>{{ toTimeString(
                            timers[12].minutes.value, timers[12].seconds.value)
                    }}</p>
                </div>
                <div class="flex flex-1 justify-between">
                    <p>Post requests:</p>
                    <p>{{ requests[0] }}</p>
                </div>
                <div class="flex flex-1 justify-between">
                    <p>Get requests:</p>
                    <p>{{ requests[1] }}</p>
                </div>
                <div class="flex flex-1 justify-between">
                    <p>Requests per second:</p>
                    <p>{{ dotnetRps }}</p>
                </div>
                <h4 class="text-center mt-4">Laravel 9</h4>
                <div class="flex flex-1 justify-between">
                    <p>Total time: </p>
                    <p>{{ toTimeString(
                            timers[13].minutes.value, timers[13].seconds.value)
                    }}</p>
                </div>
                <div class="flex flex-1 justify-between">
                    <p>Post requests:</p>
                    <p>{{ requests[2] }}</p>
                </div>
                <div class="flex flex-1 justify-between">
                    <p>Get requests:</p>
                    <p>{{ requests[3] }}</p>
                </div>
                <div class="flex flex-1 justify-between">
                    <p>Requests per second:</p>
                    <p>{{ laravelRps }}</p>
                </div>
            </div>
        </div>

        <div class="border rounded-lg w-64 mx-auto order-3">
            <Score :timers="timers.slice(6, 12)" :framework="'Laravel 9'" />
        </div>
    </div>
    <div class="text-center mx-auto">
        <button
            class="min-w-auto w-36 h-10 bg-blue-500 p-2 rounded-l-lg hover:bg-blue-700 transition-colors duration-50 hover:animate-pulse ease-out text-white font-semibold"
            @click="initiateBenchmark">Benchmark</button>
        <button
            class="min-w-auto w-36 h-10 bg-red-500 p-2 rounded-r-lg hover:bg-red-700 transition-colors duration-50 hover:animate-pulse ease-out text-white font-semibold"
            @click="clearDatabase">Clear Database</button>
    </div>
    <div class="mb-4">
        <h2 class="text-center mt-6 mb-6">Output</h2>
        <div class="lg:w-[800px] w-4/5 lg:h-72 h-48 mx-auto border rounded-lg px-4 py-2 overflow-auto">
            <p v-for="message in messages">{{ message }}</p>
        </div>
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
const dotnetRps = ref(0);
const laravelRps = ref(0);

for (let i = 0; i < 14; i++) {
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

const requestPerSecond = (currentValue: number, framework: 'dotnet' | 'laravel') => {

    setTimeout(() => {
        if (framework === 'dotnet') {
            dotnetRps.value = (requests.value[0] + requests.value[1]) - currentValue;
        } else {
            laravelRps.value = laravelRps.value;
        }
    }, 1000);
}

const clearDatabase = async () => {
    messages.value = [];
    messages.value.push('Clearing ASP.NET database...');

    let agent = new Agent('asp.net');
    await agent.Demo.deleteDb();

    messages.value.push('Clearing Laravel database...');

    agent = new Agent('laravel');
    await agent.Demo.deleteDb();
    messages.value.push('Finished clearing databases');
}

const dotnetBenchmark = async () => {
    messages.value = [];
    const agent = new Agent('asp.net');

    const users = UserFactory.create(1000);
    const products = ProductFactory.create(4000);
    const orders = OrderFactory.create(5000, users, products);

    messages.value.push('Starting ASP.NET Benchmark...')

    startTimer(0);
    startTimer(1);
    startTimer(2);
    startTimer(12);

    // 0 .. 1.000 post requests
    messages.value.push('POST: ' + import.meta.env.VITE_API_DOTNET + '/users');
    for (let i = 0; i < users.length; i++) {
        await agent.Users.post(users[i]);
        requests.value[0]++;
        requestPerSecond(requests.value[0] + requests.value[1], 'dotnet');
    }

    stopTimer(0);

    // 1.000 .. 5.000 post requests
    messages.value.push('POST: ' + import.meta.env.VITE_API_DOTNET + '/products');
    for (let i = 0; i < products.length; i++) {
        await agent.Products.post(products[i]);
        requests.value[0]++;
        requestPerSecond(requests.value[0] + requests.value[1], 'dotnet');
    }

    stopTimer(1);

    // 5.000 .. 10.000 post requests
    messages.value.push('POST: ' + import.meta.env.VITE_API_DOTNET + '/orders');
    for (let i = 0; i < orders.length; i++) {
        await agent.Orders.post(orders[i]);
        requests.value[0]++;
        requestPerSecond(requests.value[0] + requests.value[1], 'dotnet');
    }

    stopTimer(2);

    startTimer(3);
    startTimer(4);
    startTimer(5);

    // 0 .. 1.000 get requests
    messages.value.push('GET: ' + import.meta.env.VITE_API_DOTNET + '/users/{id}');
    for (let i = 0; i < users.length; i++) {
        await agent.Users.get(users[i].id);
        requests.value[1]++
        requestPerSecond(requests.value[0] + requests.value[1], 'dotnet');
    }

    stopTimer(3);

    // 1.000 .. 5.000 get requests
    messages.value.push('GET: ' + import.meta.env.VITE_API_DOTNET + '/products/{id}');
    for (let i = 0; i < products.length; i++) {
        await agent.Products.get(products[i].id);
        requests.value[1]++
        requestPerSecond(requests.value[0] + requests.value[1], 'dotnet');
    }

    stopTimer(4);

    // 5.000 .. 10.000 get requests
    messages.value.push('GET: ' + import.meta.env.VITE_API_DOTNET + '/orders/{id}');
    for (let i = 0; i < orders.length; i++) {
        await agent.Orders.get(orders[i].id);
        requests.value[1]++
        requestPerSecond(requests.value[0] + requests.value[1], 'dotnet');
    }

    stopTimer(5);
    stopTimer(12);

    messages.value.push('Finished ASP.NET Benchmark')
}

const laravelBenchmark = async () => {
    const agent = new Agent('laravel');

    const users = UserFactory.createSnake(1000);
    const products = ProductFactory.createSnake(4000);
    const orders = OrderFactory.createSnake(5000, users, products);

    messages.value.push('Starting Laravel Benchmark...')

    startTimer(6);
    startTimer(7);
    startTimer(8);
    startTimer(13);

    // 0 .. 1.000 post requests
    messages.value.push('POST: ' + import.meta.env.VITE_API_LARAVEL + '/users');
    for (let i = 0; i < users.length; i++) {
        await agent.Users.post(users[i]);
        requests.value[2]++;
        requestPerSecond(requests.value[2] + requests.value[3], 'laravel');
    }

    stopTimer(6);

    // 1.000 .. 5.000 post requests
    messages.value.push('POST: ' + import.meta.env.VITE_API_LARAVEL + '/products');
    for (let i = 0; i < products.length; i++) {
        await agent.Products.post(products[i]);
        requests.value[2]++;
        requestPerSecond(requests.value[2] + requests.value[3], 'laravel');
    }

    stopTimer(7);

    // 5.000 .. 10.000 post requests
    messages.value.push('POST: ' + import.meta.env.VITE_API_LARAVEL + '/orders');
    for (let i = 0; i < orders.length; i++) {
        await agent.Orders.post(orders[i]);
        requests.value[2]++;
        requestPerSecond(requests.value[2] + requests.value[3], 'laravel');
    }

    stopTimer(8);

    startTimer(9);
    startTimer(10);
    startTimer(11);

    // 0 .. 1.000 get requests
    messages.value.push('GET: ' + import.meta.env.VITE_API_LARAVEL + '/users/{id}');
    for (let i = 0; i < users.length; i++) {
        await agent.Users.get(users[i].id);
        requests.value[3]++
        requestPerSecond(requests.value[2] + requests.value[3], 'laravel');
    }

    stopTimer(9);

    // 1.000 .. 5.000 get requests
    messages.value.push('GET: ' + import.meta.env.VITE_API_LARAVEL + '/products/{id}');
    for (let i = 0; i < products.length; i++) {
        await agent.Products.get(products[i].id);
        requests.value[3]++
        requestPerSecond(requests.value[2] + requests.value[3], 'laravel');
    }

    stopTimer(10);

    // 5.000 .. 10.000 get requests
    messages.value.push('GET: ' + import.meta.env.VITE_API_LARAVEL + '/orders/{id}');
    for (let i = 0; i < orders.length; i++) {
        await agent.Orders.get(orders[i].id);
        requests.value[3]++
        requestPerSecond(requests.value[2] + requests.value[3], 'laravel');
    }

    stopTimer(11);

    stopTimer(13);
    messages.value.push('Finished Laravel Benchmark')
}
</script>
