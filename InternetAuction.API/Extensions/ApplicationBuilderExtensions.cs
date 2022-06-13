using System;
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
                        switch (exception)
                        {
                            case SignInException:
                                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                                break;

                            case ArgumentException:
                                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                                break;

                            default:
                                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                                break;
                        }

                        await context.Response.WriteAsJsonAsync(new { Message = exception.Message} );
                    }
                });
            });
        } 
    }
}