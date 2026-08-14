
using APICep.Exceptions;
using APICep.Loggers;

namespace APICep.Middlewares
{
    public class ExcecaoMiddleware 
    {
        private readonly RequestDelegate _proximo;
        private readonly ILogger<ExcecaoMiddleware> _registrador;
        private readonly IHostEnvironment _ambiente;

        public ExcecaoMiddleware(RequestDelegate proximo, ILogger<ExcecaoMiddleware> registrador, IHostEnvironment ambiente)
        {
            _proximo = proximo;
            _registrador = registrador;
            _ambiente = ambiente;
        }

        private async Task EscreverErroAsync(
            HttpContext httpContext,
            Exception excecao,
            int codigoStatus,
            string mensagemProducao,
            LogLevel nivelLog)
        {
            _registrador.Log(
                nivelLog,
                excecao,
                "Erro ao processar a requisição:{Mensagem}",
                excecao.Message);

            httpContext.Response.StatusCode = codigoStatus;
            httpContext.Response.ContentType = "application/json";

            var erroResposta = new ApiLogger(codigoStatus, mensagemProducao);
            await httpContext.Response.WriteAsJsonAsync(erroResposta);
        }
     
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _proximo(httpContext);
            }
            catch (FormatoIncorretoException exFormatoIncorreto)
            {
                await EscreverErroAsync(
                    httpContext,
                    exFormatoIncorreto,
                    StatusCodes.Status400BadRequest,
                    exFormatoIncorreto.Message,
                    LogLevel.Warning);
            }
            catch (CepNaoEncontradoException exCepNaoEncontrado)
            {
                await EscreverErroAsync(
                    httpContext,
                    exCepNaoEncontrado,
                    StatusCodes.Status404NotFound,
                    exCepNaoEncontrado.Message,
                    LogLevel.Warning);
            }
            catch (ApiExternaException exApiExterna)
            {
                await EscreverErroAsync(
                    httpContext,
                    exApiExterna,
                    StatusCodes.Status500InternalServerError,
                    "Não foi possível consultar o serviço de CEP.",
                    LogLevel.Error
                );
            }
            catch (Exception ex)
            {
                await EscreverErroAsync(
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
