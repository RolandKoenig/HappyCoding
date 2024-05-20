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
    <h1>Weather-Data</h1>
    <table>
      <thead>
        <tr>
          <td>Date</td>
          <td>Temperature (C)</td>
          <td>Temperature (F)</td>
          <td>Summary</td>
        </tr>
      </thead>
      <tr v-for="actRow in weatherData">
        <td>{{ actRow.date }}</td>
        <td>{{ actRow.temperatureC }}</td>
        <td>{{ actRow.temperatureF }}</td>
        <td>{{ actRow.summary }}</td>
      </tr>
    </table>
    <button @click="fetchWeatherData">Refresh</button>
  </main>
</template>

<style scoped>
  table{
    border-spacing: 0.5rem;
  }
</style>
