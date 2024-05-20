# AspNetWithVueWeather
## Description
This is a very simple sample application based on following generated samples:
 - Vue.js default application by create-vue
 - ASP.NET Core default template for Web APIs

It uses the WeatherForecast controller form the ASP.NET Core template and consumes it by a 
simple Vue.js UI

## Run the application

1. Build Vue.js application
```bash
cd HappyCoding.AspNetWithVueWeather/WebApp

npm install
npm run build
```

2. Build and run the ASP.NET Core application
```bash
cd HappyCoding.AspNetWithVueWeather
dotnet run
```

3. Open the url http://localhost:5000/index.hml using a web browser