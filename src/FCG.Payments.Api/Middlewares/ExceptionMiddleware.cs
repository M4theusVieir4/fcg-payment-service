using FCG.Payments.Api._Common;
using FCG.Payments.Domain._Common.Exceptions;
using System.Net;

namespace FCG.Payments.Api.Middlewares;
public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    private readonly ILogger<ExceptionMiddleware> _logger;

    private readonly IWebHostEnvironment _environment;

    public ExceptionMiddleware(
        RequestDelegate next,
        ILogger<ExceptionMiddleware> logger,
        IWebHostEnvironment environment)
    {
        _next = next;
        _logger = logger;
        _environment = environment;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var errorResponse = null as ErrorResponse;

        try
        {
            await _next(context);
        }
        catch (FcgDuplicateException duplicateException)
        {
            errorResponse = new ErrorResponse(duplicateException);
        }
        catch (FcgNotFoundException notFoundException)
        {
            errorResponse = new ErrorResponse(notFoundException);
        }
        catch (FcgValidationException validationException)
        {
            errorResponse = new ErrorResponse(validationException);
        }
        catch (FcgExceptionCollection exceptionCollection)
        {
            errorResponse = new ErrorResponse(exceptionCollection);
        }
        catch (Exception exception)
        {
            errorResponse = new ErrorResponse(
                HttpStatusCode.InternalServerError,
                _environment.IsDevelopment()
                    ? exception.GetFullMessageString()
                    : "An unexpected error occurred."
            );

            _logger.LogError(
                exception,
                "Unhandled exception occurred. Path: {Path}, Method: {Method}, Error: {ErrorMessage}",
                context.Request.Path,
                context.Request.Method,
                exception.GetFullMessageString()
            );
        }

        if (errorResponse is null) return;

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)errorResponse.StatusCode;

        await context.Response.WriteAsJsonAsync(errorResponse);
    }
}
