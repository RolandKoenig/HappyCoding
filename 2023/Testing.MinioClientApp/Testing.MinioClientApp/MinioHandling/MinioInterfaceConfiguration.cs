namespace Testing.MinioClientApp.MinioHandling;

public class MinioInterfaceConfiguration
{
    public string Endpoint { get; set; } = string.Empty;
    
    public string AccessKey { get; set; } = string.Empty;
    
    public string SecretKey { get; set; } = string.Empty;
    
    public bool UseSsl { get; set; } = false;
    
    public string Bucket { get; set; } = string.Empty;
}