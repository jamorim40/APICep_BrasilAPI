using APICep.Clients;
using APICep.DTOs;
using APICep.Exceptions;

namespace APICep.Services;

public class CepService:ICepService
{
    private readonly IBrasilApiClient _brasilApiClient;

    public CepService(IBrasilApiClient brasilApiClient)
    {
        _brasilApiClient = brasilApiClient;
    }
    public async Task<CepResponse> BuscarCepAsync(string cep)
    {
        
        var cepEncontrado = await _brasilApiClient.BuscarCepAsync(cep);
        if(cepEncontrado is null)
        {
            throw new CepNaoEncontradoException("CEP não encontrado.");
        }

        return new CepResponse
        {
            Cep = cepEncontrado.Cep,
            Logradouro = cepEncontrado.Street?? string.Empty,
            Bairro = cepEncontrado.Neighborhood?? string.Empty,
            Cidade = cepEncontrado.City?? string.Empty,
            Estado = cepEncontrado.State?? string.Empty
        };
    }
}
