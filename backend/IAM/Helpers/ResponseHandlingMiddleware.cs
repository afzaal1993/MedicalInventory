using Newtonsoft.Json;
using System.Net;

namespace IAM.Helpers
{
    public class ResponseHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ResponseHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                // Set the response status code
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                // Create the error response
                var errorResponse = ApiResponse<string>.Error("An error occurred while processing your request.");


                // Serialize the error response to JSON
                var json = JsonConvert.SerializeObject(errorResponse);

                // Set the response content type to JSON
                context.Response.ContentType = "application/json";

                // Write the JSON response to the client
                await context.Response.WriteAsync(json);
            }
        }
    }
}
