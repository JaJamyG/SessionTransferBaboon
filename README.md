#Session Transfer Baboon

This project is pureply an example how to transfer session data while you are migrating from ASP.NET to ASP.NET Core.

ASP.NET => .NET Framework 4.8
ASP.NET Core => .NET 6

##Nuget's

Microsoft.AspNetCore.SystemWebAdapters.CoreServices for the ASP.NET Core
Microsoft.AspNetCore.SystemWebAdapters.FrameworkServices for the ASP.NET

##Startup files

First of all you will start in the `Program.cs`. You must add the SystemWebAdapter.

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
