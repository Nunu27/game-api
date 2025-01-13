using System.Text.Json;
using GameApi.Models;

namespace GameApi.Middlewares;

public class ResponseMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        var originalStream = context.Response.Body;
        context.Response.Body = new MemoryStream();
        await next(context);

        if (!context.Request.Path.StartsWithSegments("/api")) return;

        var stream = context.Response.Body;
        context.Response.Body = originalStream;
        stream.Seek(0, SeekOrigin.Begin);

        var isSuccess = context.Response.StatusCode >= 200 && context.Response.StatusCode < 300;
        var json = stream.Length == 0 ? null : await JsonDocument.ParseAsync(stream);

        var result = new RestResponseDTO<JsonElement?>
        {
            Success = isSuccess,
            Data = isSuccess ? json?.RootElement : null,
        };

        if (json?.RootElement.ValueKind == JsonValueKind.Object)
        {
            json.RootElement.TryGetProperty("title", out var title);

            result.Message = title.GetString();
        }


        await context.Response.WriteAsJsonAsync(result);
    }


}
