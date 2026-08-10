
using APICep.Exceptions;

namespace APICep.Middlewares
{
    public class ExceptionMiddleware 
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
            await _next(httpContext);
                
            }
            catch (FormatoIncorretoException ExFormatoIncorreto)
            {
                httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                httpContext.Response.ContentType = "application/json";
                var errorResponse = new {Message = ExFormatoIncorreto.Message};
                await httpContext.Response.WriteAsJsonAsync(errorResponse);
            }
            catch (CepNaoEncontradoException ExCepNaoEncontrado)
            {
                httpContext.Response.StatusCode = StatusCodes.Status404NotFound; 
                httpContext.Response.ContentType = "application/json";
                var errorResponse = new {Message = ExCepNaoEncontrado.Message};
                await httpContext.Response.WriteAsJsonAsync(errorResponse);
            }
            
            catch (ApiExternaException ExApiExterna)
            {
                httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError; 
                httpContext.Response.ContentType = "application/json";
                var errorResponse = new {Message = ExApiExterna.Message};
                await httpContext.Response.WriteAsJsonAsync(errorResponse);
            }
            
        }
    }
}
