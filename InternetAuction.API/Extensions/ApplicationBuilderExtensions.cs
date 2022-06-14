using System;
using System.Text.Json;
using InternetAuction.BLL.Exceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using JsonSerializer = Newtonsoft.Json.JsonSerializer;

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
                            case SignInException e:
                                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                                break;

                            case ArgumentException e:
                                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                                break;

                            default:
                                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                                break;
                        }

                        await context.Response.WriteAsync(JsonConvert.SerializeObject(new {Message = exception.Message},
                            new JsonSerializerSettings() {ContractResolver = new CamelCasePropertyNamesContractResolver()}));
                    }
                });
            });
        } 
    }
}