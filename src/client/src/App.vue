<script setup lang="ts">
// This starter template is using Vue 3 <script setup> SFCs
// Check out https://vuejs.org/api/sfc-script-setup.html#script-setup
import Benchmark from './components/Benchmark.vue';
</script>

<template>
  <Suspense>
    <div class="lg:max-w-[60%] max-w-full mx-auto my-0">
      <Benchmark />
    </div>
  </Suspense>
</template>

<style>
@import url('https://fonts.googleapis.com/css2?family=IBM+Plex+Sans:ital,wght@0,300;0,400;0,500;0,600;0,700;1,300;1,400&display=swap');

@tailwind base;
@tailwind components;
@tailwind utilities;

* {
  font-size: 100%;
  font-family: 'IBM Plex Sans';
  letter-spacing: 0;
}

h1 {
  @apply text-5xl font-bold leading-[64px];
}

h2 {
  @apply text-[32px] font-bold leading-10;
}

h3 {
  @apply text-xl font-bold leading-6
}

h4 {
  @apply text-lg font-bold leading-6
}

p {
  @apply text-base font-normal leading-8;
}
</style>
