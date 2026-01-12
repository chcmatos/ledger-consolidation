using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Shared.Auth.Serialization;

namespace Shared.Auth.DependencyInjection;

public static class SwaggerDateOnlyExtensions
{
    public static void AddDateOnlySupport(this IServiceCollection services)
    {
        services.Configure<JsonOptions>(options =>
        {
            options.JsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter());
        });
    }

    public static void AddDateOnlySwaggerMapping(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.MapType<DateOnly>(() => new OpenApiSchema
            {
                Type = "string",
                Format = "date",
                Example = new Microsoft.OpenApi.Any.OpenApiString("2026-01-11")
            });
        });
    }
}
