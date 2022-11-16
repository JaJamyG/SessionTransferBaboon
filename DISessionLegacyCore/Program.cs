var builder = WebApplication.CreateBuilder(args);

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

builder.Services.AddReverseProxy().LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

builder.Services.AddTransient<ISessionCompatibilization, SessionCompatibilization>();
builder.Services.AddTransient<IGiveMeBaboon, GiveMeBaboon>();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSession();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();
app.UseRouting();
app.UseAuthorization();
app.UseSystemWebAdapters();

app.MapDefaultControllerRoute().RequireSystemWebAdapterSession();
app.MapReverseProxy();

app.Run();
