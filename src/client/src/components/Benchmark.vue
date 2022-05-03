<script lang="ts" setup>
import { ProductFactory } from '../models/product';
import Score from './Score.vue';
import { ref } from 'vue'
import Agent from '../api/agent';
import { useStopwatch } from 'vue-timer-hook'

const initiateBenchmark = async () => {
    await dotnetBenchmark();
}

const timer = useStopwatch(0, false);

function startTimer() {
    timer.start();
}

const dotnetBenchmark = async () => {
    const agent = new Agent('asp.net');
    messages.value.push('Starting ASP.NET Benchmark...')
    const products = ProductFactory.create(500);

    await agent.Demo.deleteDb();

    startTimer();
    messages.value.push('Finished ASP.NET Benchmark')
}

const messages = ref(['']);
</script>

<template>
    <div class="text-center mx-auto my-4">
        <h1>ASP.NET vs Laravel Benchmarks</h1>
    </div>
    <div class="grid grid-cols-2 text-center">
        <Score :timer="timer" :framework="'ASP.NET Core 6.0'" />
        <Score :timer="timer" :framework="'Laravel 9'" />
    </div>
    <div class="text-center mx-auto">
        <button @click="initiateBenchmark">Benchmark</button>
    </div>
    <div class="w-72 mx-auto">
        <h2 class="text-center">Output</h2>
        <p v-for="message in messages">{{ message }}</p>
    </div>
</template>

