namespace Training.FileExplorer.Api.Configurations;

public static partial class HostConfiguration
{
    public static ValueTask<WebApplicationBuilder> ConfigureAsync(this WebApplicationBuilder builder)
    {
        builder
            .AddMapping()
            .AddBrokers()
            .AddFileStorageInfrastructure()
            .AddDevTools()
            .AddExposers()
            .AddCors();
        
        return new ValueTask<WebApplicationBuilder>(builder);
    }

    public static ValueTask<WebApplication> ConfigureAsync(this WebApplication app)
    {
        app
            .UseDevTools()
            .MapRoutes()
            .UseCors()
            .UseFileStorage()
            .UseStaticFiles();
        
        return new ValueTask<WebApplication>(app);
    }
}