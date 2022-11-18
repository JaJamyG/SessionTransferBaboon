# Session Transfer Baboon

This project is pureply an example how to transfer session data while you are migrating from ASP.NET to ASP.NET Core.

ASP.NET => .NET Framework 4.8

ASP.NET Core => .NET 6

## Nuget's

Microsoft.AspNetCore.SystemWebAdapters.CoreServices for the ASP.NET Core
Microsoft.AspNetCore.SystemWebAdapters.FrameworkServices for the ASP.NET

## Startup files

First of all you will start in the `Program.cs`. You must add the SystemWebAdapter to the services.

```csharp
builder.Services.AddSystemWebAdapters()
    .AddJsonSessionSerializer(options =>
    {
        // Serialization/deserialization requires each session key to be registered to a type
        options.RegisterKey<string>("Baboon");
    })
    .AddRemoteAppClient(options =>
    {
        // Provide the URL for the remote app that has enabled session querying
        options.RemoteAppUrl =
            new(builder.Configuration["ReverseProxy:Clusters:fallbackCluster:Destinations:fallbackApp:Address"]);

        // Provide a strong API key that will be used to authenticate the request on the remote app for querying the session
        options.ApiKey = "6b5b73c9-454e-4916-918e-34e16e27e72f";
    })
    .AddSessionClient();
```

Don't forget to also add it at the app itself.

```csharp
app.MapDefaultControllerRoute().RequireSystemWebAdapterSession();
```

After this you can switch to the `global.asax.cs`.

```csharp
protected void Application_Start()
        {
            //... other code ...

            SystemWebAdapterConfiguration.AddSystemWebAdapters(this)
                .AddProxySupport(options => options.UseForwardedHeaders = true)
                .AddJsonSessionSerializer(options =>
                {
                    options.RegisterKey<string>("Baboon");
                })
                .AddRemoteAppServer(options => options.ApiKey = "6b5b73c9-454e-4916-918e-34e16e27e72f")
                .AddSessionServer();
        }
```

Make sure the ApiKey is the same and is a guid.

## How to use it

In the ASP.NET project it's all the same. But in the ASP.NET Core you will see a difference instead of a non-static class you will use the System.Web.HttpContext again.

In order to have access in the business class you will need to make a service. We will build a interface in the .NET Standard library and a class who implements the  

### .NET Standard
```csharp
public interface ISessionCompatibilization
    {
        byte[] Get(string key);
        int? GetInt32(string key);
        string GetString(string key);
        void SetInt32(string key, int value);
        void SetString(string key, string value);
        bool IsAvailable { get; }
        string Id { get; }
        IEnumerable<string> Keys { get; }
        void Clear();
        Task CommitAsync(CancellationToken cancellationToken = default(CancellationToken));
        Task LoadAsync(CancellationToken cancellationToken = default(CancellationToken));
        void Remove(string key);
        void Set(string key, byte[] value);
        bool TryGetValue(string key, out byte[] value);
    }
```

### ASP.NET
```csharp
public class SessionCompatibilization : ISessionCompatibilization
    {
        public bool IsAvailable => throw new NotImplementedException();

        public string Id => HttpContext.Current.Session.SessionID;

        public IEnumerable<string> Keys => (IEnumerable<string>)HttpContext.Current.Session.Keys;

        public void Clear()
        {
            HttpContext.Current.Session.Clear();
        }

        public Task CommitAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public byte[] Get(string key)
        {

            return (byte[])HttpContext.Current.Session[key];

        }

        public int? GetInt32(string key)
        {
            throw new NotImplementedException();
        }

        public string GetString(string key)
        {
            return (string)HttpContext.Current.Session[key];
        }

        public Task LoadAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public void Remove(string key)
        {
            HttpContext.Current.Session.Remove(key);
        }

        public void Set(string key, byte[] value)
        {
            HttpContext.Current.Session[key] = value;
        }

        public void SetInt32(string key, int value)
        {
            throw new NotImplementedException();
        }

        public void SetString(string key, string value)
        {
            HttpContext.Current.Session[key] = value;
        }

        public bool TryGetValue(string key, out byte[] value)
        {
            throw new NotImplementedException();
        }
    }
```

### ASP.NET Core
```csharp
    public class SessionCompatibilization : ISessionCompatibilization
    {
        public byte[] Get(string key)
        {
            throw new NotImplementedException();
        }

        public int? GetInt32(string key)
        {
            throw new NotImplementedException();
        }

        public string GetString(string key)
        {
            return (string)System.Web.HttpContext.Current.Session[key];
        }

        public void SetInt32(string key, int value)
        {
            throw new NotImplementedException();
        }

        public void SetString(string key, string value)
        {
            throw new NotImplementedException();
        }

        public bool IsAvailable { get; }
        public string Id { get; }
        public IEnumerable<string> Keys { get; }
        public void Clear()
        {
            throw new NotImplementedException();
        }

        public Task CommitAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public Task LoadAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public void Remove(string key)
        {
            throw new NotImplementedException();
        }

        public void Set(string key, byte[] value)
        {
            throw new NotImplementedException();
        }

        public bool TryGetValue(string key, out byte[] value)
        {
            throw new NotImplementedException();
        }
    }
```

Now you can use it on both projects and everything is set up!
