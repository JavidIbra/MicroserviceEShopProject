using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MicroserviceEShopProject.BuildingBlocks.Exceptions.Handler
{
    public sealed class ProblemExceptionHandler(
    IProblemDetailsService problemDetailsService,
     ILogger<ProblemExceptionHandler> logger) : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext,
            Exception exception,
            CancellationToken cancellationToken)
        {
            logger.LogError(exception, "Error Message: {Message} , Occured at : {Time}", exception.Message, DateTime.UtcNow);

            httpContext.Response.StatusCode = exception switch
            {
                ApplicationException => StatusCodes.Status400BadRequest,
                FluentValidation.ValidationException => StatusCodes.Status400BadRequest,
                _ => StatusCodes.Status500InternalServerError,
            };

            return await problemDetailsService.TryWriteAsync(
                new ProblemDetailsContext
                {
                    HttpContext = httpContext,
                    Exception = exception,
                    ProblemDetails = new ProblemDetails
                    {
                        Type = exception.GetType().Name,
                        Title = "An Error Occured",
                        Detail = exception.Message,
                    }
                }
            );
        }
    }
}
