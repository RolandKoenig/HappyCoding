<script setup lang="ts">
  import {inject, type Ref, ref} from "vue";
  import {WeatherApiClientAxios} from "@/services/WeatherApiClientAxios";
  import type {WeatherForecastDto} from "@/services/WeatherForecastDto";

  const weatherApiClient = inject<WeatherApiClientAxios>("WeatherApiClient");
  
  const weatherData: Ref<WeatherForecastDto[]> = ref([]);

  async function fetchWeatherData() {
    weatherData.value = await weatherApiClient?.getWeatherForecast() ?? [];
  }
  
  fetchWeatherData();
</script>

<template>
  <main>
    <h1 class="text-center">Weather-Data</h1>
    <div class="container">
      <div class="row">
        <div class="col-1 fw-bold">#</div>
        <div class="col-2 fw-bold">Date</div>
        <div class="col-3 fw-bold">Temperature (C)</div>
        <div class="col-3 fw-bold">Temperature (F)</div>
        <div class="col-3 fw-bold">Summary</div>
      </div>
      <div v-for="(actRow, index) in weatherData">
        <div class="row">
          <div class="col-1">{{ index + 1 }}</div>
          <div class="col-2">{{ actRow.date }}</div>
          <div class="col-3">{{ actRow.temperatureC }}</div>
          <div class="col-3">{{ actRow.temperatureF }}</div>
          <div class="col-3">{{ actRow.summary }}</div>
        </div>
      </div>
      <div class="row">
        <div class="col-12">
          <br />
          <button @click="fetchWeatherData"
                  class="btn btn-primary">
            Refresh
          </button>
        </div>
      </div>
    </div>
  </main>
</template>

<style scoped>
  table{
    border-spacing: 0.5rem;
  }
</style>
