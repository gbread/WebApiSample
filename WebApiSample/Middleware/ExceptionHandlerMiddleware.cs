using Newtonsoft.Json;
using System.Net;
using WebApiSample.BLL.Exceptions;

namespace WebApiSample.Middleware
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }


        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {

                await ConvertException(context, exception);
            }
        }

        private Task ConvertException(HttpContext context, Exception exception)
        {
            HttpStatusCode httpsStatusCode = HttpStatusCode.InternalServerError;

            context.Response.ContentType = "application/json";

            var result = string.Empty;

            switch (exception)
            {
                // TODO:
                //case ModelValidationException validationException:
                //    httpsStatusCode = HttpStatusCode.BadRequest;
                //    result = JsonConvert.SerializeObject(validationException.ValdationErrors);
                //    break;
                case ModelNotFoundException notFoundException:
                    httpsStatusCode = HttpStatusCode.NotFound;
                    break;
                default:
                    httpsStatusCode = HttpStatusCode.BadRequest;
                    break;
            }

            context.Response.StatusCode = (int)httpsStatusCode;

            if (result == string.Empty)
            {
                result = JsonConvert.SerializeObject(new { error = exception.Message });

            }

            return context.Response.WriteAsync(result);
        }
    }
}
