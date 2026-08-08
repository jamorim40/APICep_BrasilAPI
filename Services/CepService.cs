using APICep.Clients;
using APICep.DTOs;
using APICep.Exceptions;

namespace APICep.Services;

public class CepService:ICepService
{
    private readonly IBrasilApiClient _brasilClient;

    public CepService(IBrasilApiClient brasilApiClient)
    {
        _brasilClient = brasilApiClient;
    }
    public async Task<CepResponse> BuscarCep(string cep)
    {
        var resultado=await _brasilClient.BuscarCep(cep);
        if(resultado is null)
        {
            throw new CepNaoEncontradoException("CEP não encontrado.");
        }

        return new CepResponse
        {
            Cep = resultado.Cep,
            Logradouro = resultado.Street?? string.Empty,
            Bairro = resultado.Neighborhood?? string.Empty,
            Cidade = resultado.City?? string.Empty,
            Estado = resultado.State?? string.Empty
        };
    }
}
