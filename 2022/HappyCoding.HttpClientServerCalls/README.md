This project shows which http headers are genereated by HttpClient class.
The client is created like this
```csharp
var client = new HttpClient();
client.BaseAddress = new Uri("http://localhost:5001");
```

## Get
```csharp
await client.GetAsync("dummyEndpoint");
```

procudes http headers:
```
Host: localhost:5001
```

## PostJsonObject
```csharp
await client.PostAsJsonAsync("dummyEndpoint", new DummyRequestObject());
```

procudes http headers:
```
Host: localhost:5001
Content-Type: application/json; charset=utf-8
Transfer-Encoding: chunked
```

## PutJsonObject
```csharp
await client.PutAsJsonAsync("dummyEndpoint", new DummyRequestObject());
```

procudes http headers:
```
Host: localhost:5001
Content-Type: application/json; charset=utf-8
Transfer-Encoding: chunked
```

## Delete
```csharp
await client.DeleteAsync("dummyEndpoint");
```

procudes http headers:
```
Host: localhost:5001
```

## UploadFile
```csharp
using var multipartFormContent = new MultipartFormDataContent();

//Load the file and set the file's Content-Type header
var fileStreamContent = new StreamContent(File.OpenRead("DummyImage.png"));
fileStreamContent.Headers.ContentType = new MediaTypeHeaderValue("image/png");

//Add the file
multipartFormContent.Add(fileStreamContent, name: "file", fileName: "house.png");

//Send it
await client.PostAsync("dummyEndpoint", multipartFormContent);
```

procudes http headers:
```
Host: localhost:5001
Content-Type: multipart/form-data; boundary="1299be49-7d4f-4ea0-b6b8-aeea9a4669e3"
Content-Length: 629
```