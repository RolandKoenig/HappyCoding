<script setup lang="ts">
  import {inject, type Ref, ref} from "vue";
  import {WeatherApiClient, WeatherForecast} from "@/services/WeatherApiClient.generated";

  const weatherApiClient = inject<WeatherApiClient>("WeatherApiClient");
  
  const weatherData: Ref<WeatherForecast[]> = ref([]);
  
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
        <div class="col-3 fw-bold">Date</div>
        <div class="col-3 fw-bold">Temperature (C)</div>
        <div class="col-3 fw-bold">Temperature (F)</div>
        <div class="col-3 fw-bold">Summary</div>
      </div>
      <div v-for="actRow in weatherData">
        <div class="row">
          <div class="col-3">{{ actRow.date }}</div>
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
