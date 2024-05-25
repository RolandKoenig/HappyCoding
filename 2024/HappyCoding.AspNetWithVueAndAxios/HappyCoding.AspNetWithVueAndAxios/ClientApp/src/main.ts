import "bootstrap/dist/css/bootstrap.min.css"

import {createApp} from 'vue'
import App from './App.vue'
import {WeatherApiClientAxios} from "@/services/WeatherApiClientAxios";

let app = createApp(App);
app.provide("WeatherApiClient", new WeatherApiClientAxios("/api"));
app.mount('#app')
