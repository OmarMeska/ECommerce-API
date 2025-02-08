//using EcommerceProject.Exceptions;
using EcommerceProject.Exceptions;
using System.Net;
using System.Text.Json;

namespace EcommerceProject.Configurations
{
    public class GlobalExceptionHandlerMiddleware
    {
        private readonly RequestDelegate Next;
        public GlobalExceptionHandlerMiddleware(RequestDelegate next)
        {
            Next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await Next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }
        private static Task HandleExceptionAsync(HttpContext context, Exception ex) {

            HttpStatusCode status = HttpStatusCode.NotAcceptable; 
            var ExMessage = ex.Message;
            var StackTrace = ex.StackTrace;

            var exceptionType = ex.GetType();

            if (exceptionType == typeof(Exceptions.NotFoundException) ||
                exceptionType == typeof(Exceptions.KeyNotFoundException)) 
                status= HttpStatusCode.NotFound;
           else if (exceptionType == typeof(Exceptions.BadRequestException))
                status= HttpStatusCode.BadRequest;
            else if (exceptionType == typeof(Exceptions.NotImplementedException))
                status = HttpStatusCode.NotImplemented;
            else if (exceptionType == typeof(Exceptions.UnauthorizedAccessException))
                status = HttpStatusCode.Unauthorized;

            var result = JsonSerializer.Serialize(new { errormessage = ExMessage, StackTrace });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)status;

            return context.Response.WriteAsync(result);
        }
    }
}
