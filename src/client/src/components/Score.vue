<script lang="ts" setup>
import RequestWithTimer from './RequestWithTimer.vue';
import { toTimeString } from '../util';

const props = defineProps({
    framework: String,
    timers: {
        type: Array,
    }
});

</script>

<template>
    <div class="p-6">
        <h3>{{ props.framework }}</h3>
        <h4 class="mt-4">Write requests</h4>
        <div class="w-44 mx-auto">
            <RequestWithTimer :request-count="'1.000'"
                :timer="toTimeString(props.timers[0].minutes.value, props.timers[0].seconds.value)" />
            <RequestWithTimer :request-count="'5.000'"
                :timer="toTimeString(props.timers[1].minutes.value, props.timers[1].seconds.value)" />
            <RequestWithTimer :request-count="'10.000'"
                :timer="toTimeString(props.timers[2].minutes.value, props.timers[2].seconds.value)" />
        </div>

        <h4 class="mt-4">Get requests</h4>
        <div class="w-44 mx-auto">
            <RequestWithTimer :request-count="'1.000'"
                :timer="toTimeString(props.timers[3].minutes.value, props.timers[3].seconds.value)" />
            <RequestWithTimer :request-count="'5.000'"
                :timer="toTimeString(props.timers[4].minutes.value, props.timers[4].seconds.value)" />
            <RequestWithTimer :request-count="'10.000'"
                :timer="toTimeString(props.timers[5].minutes.value, props.timers[5].seconds.value)" />
        </div>
    </div>
</template>