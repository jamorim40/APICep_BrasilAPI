using APICep.DTOs;
using APICep.Exceptions;
using System.Net;
using System.Text.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace APICep.Clients
{
    public class BrasilApiClient:IBrasilApiClient
    {
        private readonly HttpClient _clienteHttp;

        public BrasilApiClient(HttpClient clienteHttp)
        {
            _clienteHttp = clienteHttp;
        }

        public async Task<BrasilCepResponse?> BuscarCepAsync(string cep)
        {
            var resposta = await _clienteHttp.GetAsync($"api/cep/v2/{cep}");

            if (resposta.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }
            if (resposta.StatusCode == HttpStatusCode.BadRequest)
            {
                throw new FormatoIncorretoException("CEP em formato inválido.");
            }
            if (resposta.StatusCode == HttpStatusCode.InternalServerError)
            {
                throw new ApiExternaException("Cliente externos apresentou uma falha.");
            }

            var conteudoJson = await resposta.Content.ReadAsStringAsync();
            var brasilCepResposta = JsonSerializer.Deserialize<BrasilCepResponse>(conteudoJson,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            });

            return brasilCepResposta;
        }
    }
}
