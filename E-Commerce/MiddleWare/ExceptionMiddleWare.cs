using System.Net;
using System.Text.Json;
using E_Commerce.Errors;

namespace E_Commerce.MiddleWare;

public class ExceptionMiddleWare
{
    private readonly IHostEnvironment env;
    private readonly ILogger<ExceptionMiddleWare> logger;
    private readonly RequestDelegate next;

    public ExceptionMiddleWare(RequestDelegate next, IHostEnvironment env, ILogger<ExceptionMiddleWare> logger)
    {
        this.next = next;
        this.env = env;
        this.logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, ex.Message);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            var response = env.IsDevelopment()
                ? new ApiException((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace)
                : new ApiException((int)HttpStatusCode.InternalServerError);
            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            var json = JsonSerializer.Serialize(response, options);

            await context.Response.WriteAsync(json);
        }
    }
}