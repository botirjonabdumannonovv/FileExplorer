﻿using System.Reflection;
using FileExplorer.Application.FileStorage.Brokers;
using FileExplorer.Infrastructure.FileStorage.Brokers;

namespace FileExplorer.Api.Configurations;

public static partial class HostConfiguration
{
    private static WebApplicationBuilder AddMapping(this WebApplicationBuilder builder)
    {
        var assemblies = Assembly.GetExecutingAssembly().GetReferencedAssemblies().Select(Assembly.Load).ToList();

        assemblies.Add(Assembly.GetExecutingAssembly());

        builder.Services.AddAutoMapper(assemblies);

        return builder;
    }

    private static WebApplicationBuilder AddBrokers(this WebApplicationBuilder builder)
    {
        builder.Services.AddSingleton<IDriveBroker, DriveBroker>();

        return builder;
    }

    private static WebApplicationBuilder AddDevTools(this WebApplicationBuilder builder)
    {
        builder.Services.AddSwaggerGen().AddEndpointsApiExplorer();

        return builder;
    }

    private static WebApplicationBuilder AddRestExposers(this WebApplicationBuilder builder)
    {
        builder.Services.AddControllers().AddNewtonsoftJson();
        builder.Services.AddRouting(options => options.LowercaseUrls = true);

        return builder;
    }

    private static WebApplicationBuilder AddCustomCors(this WebApplicationBuilder builder)
    {
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy", policyBuilder => { policyBuilder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader(); });
        });

        return builder;
    }

    private static WebApplication UseDevTools(this WebApplication app)
    {
        app.UseSwagger().UseSwaggerUI();

        return app;
    }

    private static WebApplication UseCustomCors(this WebApplication app)
    {
        app.UseCors("CorsPolicy");

        return app;
    }

    private static WebApplication MapRoutes(this WebApplication app)
    {
        app.MapControllers();

        return app;
    }

    private static WebApplication UseFileStorage(this WebApplication app)
    {
        app.UseStaticFiles();

        return app;
    }

}