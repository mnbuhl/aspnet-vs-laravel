<script setup lang="ts">
// This starter template is using Vue 3 <script setup> SFCs
// Check out https://vuejs.org/api/sfc-script-setup.html#script-setup
import Benchmark from './components/Benchmark.vue';
</script>

<template>
  <Suspense>
    <div class="max-w-[60%] mx-auto my-0">
      <Benchmark />
    </div>
  </Suspense>
</template>

<style>
@tailwind base;
@tailwind components;
@tailwind utilities;
</style>
