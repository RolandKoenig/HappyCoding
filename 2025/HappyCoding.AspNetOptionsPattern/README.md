# ASP.NET Core Options pattern
 - Options pattern is the default way to map settings from Microsoft.Extensions.Configuration to objects
 - Configure using dependency injection 
   - services.AddOptions<T>().Bind(configuration.GetSection("..."));
   - Add validation if needed
 - The consumer of a configuration object can decide, how to react on configuration changes
   - IOptions<T>: Get the values set when the application started
   - IOptionsSnapshop<T>: Get current values
   - IOptionsMonitor<T>: Subscribe for configuration changes

## Things to consider
 - Where you want to react on configuration changes during runtime of the application

## Recommendation
 - Use Options pattern with IOptions by default
 - Use IOptionsSnapshop<T> and IOptionsMonitor<T> where needed. The reaction on configurations changes should be testet