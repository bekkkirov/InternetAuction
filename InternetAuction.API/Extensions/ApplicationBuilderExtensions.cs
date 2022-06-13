using InternetAuction.BLL.Exceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;

namespace InternetAuction.API.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static void UseExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(error =>
            {
                error.Run(async context =>
                {
                    context.Response.ContentType = "application/json";

                    var exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;

                    if (exception != null)
                    {
                        if (exception is SignInException)
                        {
                            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        }

                        else
                        {
                            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                        }

                        await context.Response.WriteAsJsonAsync(new { Message = exception.Message} );
                    }
                });
            });
        } 
    }
}