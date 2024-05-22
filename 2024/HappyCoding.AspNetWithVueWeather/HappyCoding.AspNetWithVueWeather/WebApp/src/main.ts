import "bootstrap/dist/css/bootstrap.min.css"

import { createApp } from 'vue'
import { WeatherApiClient } from "@/services/WeatherApiClient.generated";
import App from './App.vue'

let app = createApp(App);
app.provide("WeatherApiClient", new WeatherApiClient());
app.mount('#app')
