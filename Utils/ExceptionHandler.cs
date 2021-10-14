using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using AlgoFit.Errors;
using Newtonsoft.Json;
using System.Text;

namespace AlgoFit.Errors.Managers
{
    public class ExceptionHandler
    {
        public static IApplicationBuilder UseAlgoFitExceptionHandler(IApplicationBuilder app)
        {
            app.Use(async (context, next) =>
            {
                var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                if (contextFeature != null)
                {
                    var exception = contextFeature.Error;

                    byte[] bytes;

                    if (exception is AlgoFitError)
                    {
                        var httpAlgoFitError = (AlgoFitError)exception;
                        context.Response.StatusCode = httpAlgoFitError.Status;
                        context.Response.ContentType = httpAlgoFitError.MessageType;

                        bytes = Encoding.ASCII.GetBytes(
                            JsonConvert.SerializeObject(httpAlgoFitError.GetErrorObject())
                                .ToString()
                        );
                    }
                    else
                    {
                        context.Response.StatusCode = 500;
                        context.Response.ContentType = "plain/text";

                        bytes = Encoding.ASCII.GetBytes("Unknown Error.");
                    }
                    await context.Response.Body.WriteAsync(bytes);
                }
            });
            return app;
        }
    }
}
