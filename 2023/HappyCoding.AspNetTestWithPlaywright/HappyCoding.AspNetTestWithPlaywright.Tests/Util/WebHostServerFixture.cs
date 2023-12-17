using System.Runtime.ExceptionServices;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace HappyCoding.AspNetTestWithPlaywright.Tests.Util;

public abstract class WebHostServerFixture : IDisposable
{
    private readonly Lazy<Uri> _rootUriInitializer;

    public Uri RootUri => _rootUriInitializer.Value;
    
    private IHost? Host { get; set; }

    protected WebHostServerFixture()
    {
        _rootUriInitializer = new Lazy<Uri>(() => new Uri(StartAndGetRootUri()));
    }

    private static void RunInBackgroundThread(Action action)
    {
        using var isDone = new ManualResetEvent(false);

        ExceptionDispatchInfo edi = null;
        new Thread(() =>
        {
            try
            {
                action();
            }
            catch (Exception ex)
            {
                edi = ExceptionDispatchInfo.Capture(ex);
            }

            isDone.Set();
        }).Start();

        if (!isDone.WaitOne(TimeSpan.FromSeconds(10)))
        {
            throw new TimeoutException("Timed out waiting for: " + action);
        }

        if (edi != null)
        {
            throw edi.SourceException;
        }
    }

    protected virtual string StartAndGetRootUri()
    {
        TestUtil.EnsureBrowsersInstalled();
        
        // As the port is generated automatically, we can use IServerAddressesFeature to get the actual server URL
        Host = CreateWebHost();
        RunInBackgroundThread(Host.Start);
        return Host.Services
            .GetRequiredService<IServer>()
            .Features
            .Get<IServerAddressesFeature>()!
            .Addresses.Single();
    }

    public virtual void Dispose()
    {
        Host?.Dispose();
        Host?.StopAsync();
    }

    protected abstract IHost CreateWebHost();
}

// ASP.NET Core with a Startup class (MVC / Pages / Blazor Server)
public class WebHostServerFixture<TStartup> : WebHostServerFixture
    where TStartup : class
{
    protected override IHost CreateWebHost()
    {
        return new HostBuilder()
            .ConfigureHostConfiguration(config =>
            {
                // Make UseStaticWebAssets work
                var applicationPath = typeof(TStartup).Assembly.Location;
                var applicationDirectory = Path.GetDirectoryName(applicationPath);

                // In ASP.NET 5, the file is named app.staticwebassets.xml
                // In ASP.NET 6, the file is named app.staticwebassets.runtime.json
                var name = Path.ChangeExtension(applicationPath, ".staticwebassets.runtime.json");
                var inMemoryConfiguration = new Dictionary<string, string>
                {
                    [WebHostDefaults.StaticWebAssetsKey] = name,
                };
                config.AddInMemoryCollection(inMemoryConfiguration);
            })
            .ConfigureWebHost(webHostBuilder => webHostBuilder
                .UseKestrel()
                .UseSolutionRelativeContentRoot(typeof(TStartup).Assembly.GetName().Name)
                .UseStaticWebAssets()
                .UseStartup<TStartup>()
                .UseUrls($"http://127.0.0.1:0")) // :0 allows to choose a port automatically
            .Build();
    }
}