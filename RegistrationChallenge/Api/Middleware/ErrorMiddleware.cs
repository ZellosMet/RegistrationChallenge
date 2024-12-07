using Microsoft.AspNetCore.WebUtilities;
using RegistrationChallenge.Api.Messages;
using System.Net;

namespace RegistrationChallenge.Api.Middleware
{
    // ErrorMiddleware - middleware обработки ошибок
    public class ErrorMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
                // process 400
                if (context.Response.StatusCode / 100 == 4 && !context.Response.HasStarted)
                {
                    string message = ReasonPhrases.GetReasonPhrase(context.Response.StatusCode);
                    ErrorMessage error = new ErrorMessage(Type: context.Response.StatusCode.ToString(), Message: message);
                    await context.Response.WriteAsJsonAsync(error);
                }
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                ErrorMessage error = new ErrorMessage(Type: ex.GetType().Name, Message:  ex.Message);
                await context.Response.WriteAsJsonAsync(error);
            }
        }
    }
}
