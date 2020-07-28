using Microsoft.AspNetCore.Http;
using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Filters;
using YOBA_LibraryData.DAL.Exceptions;
using Microsoft.Extensions.Logging;
using System.Security.Claims;

namespace YOBA_Web.Filters
{
    /// <summary>
    /// Global exception handler.
    /// Through pipeline exceptions from
    /// assemblies filtering on this
    /// middleware
    /// </summary>
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;
        public ExceptionMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<ExceptionMiddleware>();
            _next = next;
        }
        
        /// <summary>
        /// Every httpcontext moving
        /// throught this method and in
        /// case of exception triggering
        /// HandleException method for
        /// create response about exception
        /// and logging into txt file
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                var userId = httpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                _logger.LogError($"{DateTime.Now} EXCEPTION. UserId: {userId}. \nErrorMessage: {ex}");
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            return context.Response.WriteAsync(new ErrorDetails()
            {
                StatusCode = context.Response.StatusCode,
                Message = $"Middleware Exception: {exception.Message}"
            }.ToString());
        }
    }
}
