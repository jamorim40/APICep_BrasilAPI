using APICep.DTOs;
using APICep.Exceptions;
using System.Net;
using System.Text.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace APICep.Clients
{
    public class BrasilApiClient:IBrasilApiClient
    {
        private readonly HttpClient _httpClient;
        public BrasilApiClient(HttpClient httpClient) 
        {
            _httpClient = httpClient;
        }

        public async Task<BrasilCepResponse?> BuscarCep(string cep)
        {
            var response = await _httpClient.GetAsync($"https://brasilapi.com.br/api/cep/v2/{cep}");

            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }
            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                throw new FormatoIncorretoException("CEP em formato inválido.");
            }
            if (response.StatusCode == HttpStatusCode.InternalServerError)
            {
                throw new ApiExternaException("Cliente externos apresentou uma falha.");
            }

            var json= await response.Content.ReadAsStringAsync();
            var resultado=JsonSerializer.Deserialize<BrasilCepResponse>(json,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            });

            return resultado;

           
        }
    }
}
