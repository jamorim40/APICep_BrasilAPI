
using APICep.Exceptions;
using APICep.Loggers;

namespace APICep.Middlewares
{
    public class ExceptionMiddleware 
    {
        private readonly RequestDelegate _next;
        private readonly ILogger <ExceptionMiddleware> _logger;
        private readonly IHostEnvironment _env;

        public ExceptionMiddleware(RequestDelegate next,ILogger<ExceptionMiddleware> logger, IHostEnvironment env )
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        private async Task EscreverErrorAsync(
            HttpContext httpContext, 
            Exception exception, 
            int statusCode, 
            string mensagemProducao,
            LogLevel logLevel)
        {
            _logger.Log(
                logLevel,
                exception, 
                "Erro ao processar a requisição:{Mensagem}"
                ,exception.Message);

            httpContext.Response.StatusCode = statusCode;
            httpContext.Response.ContentType = "application/json";


            var errorResponse = new ApiLogger(statusCode, mensagemProducao);
            await httpContext.Response.WriteAsJsonAsync(errorResponse);
        }
     
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
            await _next(httpContext);
                
            }
            catch (FormatoIncorretoException ExFormatoIncorreto)
            {
                await EscreverErrorAsync(
                    httpContext,
                    ExFormatoIncorreto,
                    StatusCodes.Status400BadRequest,
                    ExFormatoIncorreto.Message,
                    LogLevel.Warning);
            }
            catch (CepNaoEncontradoException ExCepNaoEncontrado)
            {
                await EscreverErrorAsync(
                    httpContext,
                    ExCepNaoEncontrado,
                    StatusCodes.Status404NotFound,
                    ExCepNaoEncontrado.Message,
                    LogLevel.Warning);        
            }
            
            catch (ApiExternaException ExApiExterna)
            {
                await EscreverErrorAsync(
                    httpContext,
                    ExApiExterna,
                    StatusCodes.Status500InternalServerError,
                    "Não foi possível consultar o serviço de CEP.",
                    LogLevel.Error
                );
            }
            catch (Exception ex)
            {
                await EscreverErrorAsync(
                    httpContext,
                    ex,
                    StatusCodes.Status500InternalServerError,
                    "Erro interno no serviço CEP.",
                    LogLevel.Error
                );
        }
    }
}
}
