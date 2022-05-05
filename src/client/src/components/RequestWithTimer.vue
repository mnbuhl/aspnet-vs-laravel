<script lang="ts" setup>
const props = defineProps({
  requestCount: String,
  timer: String,
});
</script>
2
<template>
  <div class="grid grid-cols-2">
    <p>{{ requestCount }}</p>
    <p>{{ timer }}</p>
  </div>
</template>