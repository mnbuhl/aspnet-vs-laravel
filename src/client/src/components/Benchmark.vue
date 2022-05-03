<script lang="ts" setup>
import agent from '../api/agent'

const order = await agent.Orders.get("0303d232-6c1d-4c23-88db-849663ae77e6");
defineExpose({
    order
})

console.log(order);
</script>

<template>

</template>

