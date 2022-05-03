<script lang="ts" setup>
import RequestWithTimer from './RequestWithTimer.vue';


const props = defineProps({
    framework: String,
    timer: Object
});

</script>

<template>
    <div>
        <h3>{{ props.framework }}</h3>
        <h2>Write requests</h2>
        <div class="w-44 mx-auto">
            <RequestWithTimer :request-count="'1.000'"
                :timer="`${props.timer.minutes.value}:${props.timer.seconds.value}`" />
            <RequestWithTimer :request-count="'10.000'" :timer="'timer'" />
            <RequestWithTimer :request-count="'100.000'" :timer="'timer'" />
            <RequestWithTimer :request-count="'1.000.000'" :timer="'timer'" />
        </div>

        <h2>Get requests</h2>
        <div class="w-44 mx-auto">
            <RequestWithTimer :request-count="'1.000'" :timer="'timer'" />
            <RequestWithTimer :request-count="'10.000'" :timer="'timer'" />
            <RequestWithTimer :request-count="'100.000'" :timer="'timer'" />
            <RequestWithTimer :request-count="'1.000.000'" :timer="'timer'" />
        </div>
    </div>
</template>