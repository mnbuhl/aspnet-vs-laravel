<script lang="ts" setup>
import RequestWithTimer from './RequestWithTimer.vue';

const props = defineProps({
    framework: String,
    timers: {
        type: Array,
    }
});

function toTimeString(minutes: Number, seconds: Number): string {
    if (minutes < 10 && seconds < 10) {
        return `0${minutes}:0${seconds}`;
    }

    if (minutes < 10 && seconds >= 10) {
        return `0${minutes}:${seconds}`;
    }

    return `${minutes}:${seconds}`;
}


</script>

<template>
    <div>
        <h3>{{ props.framework }}</h3>
        <h2>Write requests</h2>
        <div class="w-44 mx-auto">
            <RequestWithTimer :request-count="'1.000'"
                :timer="toTimeString(props.timers[0].minutes.value, props.timers[0].seconds.value)" />
            <RequestWithTimer :request-count="'5.000'"
                :timer="toTimeString(props.timers[1].minutes.value, props.timers[1].seconds.value)" />
            <RequestWithTimer :request-count="'10.000'"
                :timer="toTimeString(props.timers[2].minutes.value, props.timers[2].seconds.value)" />
        </div>

        <h2>Get requests</h2>
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