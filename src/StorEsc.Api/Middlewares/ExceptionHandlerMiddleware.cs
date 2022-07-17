using Microsoft.AspNetCore.Diagnostics;
using StorEsc.Api.ViewModels;

namespace StorEsc.Api.Middlewares;

public static class ExceptionHandlerMiddleware
{
    public static void UseCustomExceptionHandler(this IApplicationBuilder app)
    {
        app.UseExceptionHandler(appError =>
        {
            appError.Run(async context =>
            {
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Response.ContentType = "application/json";

                var contextFeature = context.Features.Get<IExceptionHandlerFeature>();

                if (contextFeature != null)
                {
                    await context.Response.WriteAsJsonAsync(new ResultViewModel
                    {
                        Message = "An internal server error has occurred, please try again",
                        Success = false,
                        Data = new { }
                    });
                }
            });
        });
    }
}