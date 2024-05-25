import axios, {type AxiosInstance} from 'axios';
import type {WeatherForecastDto} from "@/services/WeatherForecastDto";

export class WeatherApiClientAxios{
    private _client: AxiosInstance;
    
    constructor(baseUrl?: string) {
        this._client = axios.create({
            baseURL: baseUrl,
        });
    }
    
    async getWeatherForecast() : Promise<WeatherForecastDto[]>{
        try{
            const response = await this._client.get("/WeatherForecast");
            return response.data as WeatherForecastDto[];
        }
        catch(err){
            console.error("Error requesting /api/WeatherForecast", err)
            return [];
        }
    }
}