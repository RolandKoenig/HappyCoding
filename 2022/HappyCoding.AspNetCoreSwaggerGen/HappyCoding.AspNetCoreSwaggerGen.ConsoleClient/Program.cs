using HappyCoding.AspNetCoreSwaggerGen.ConsoleClient;

var httpClient = new HttpClient();
var client = new ProductsClient("https://localhost:5001", httpClient);

var products = await client.GetProductsByProductTypeAsync(ProductType.Software);

Console.ReadLine();