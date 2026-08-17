using APICep.Clients;
using APICep.DTOs;
using APICep.Exceptions;
using Microsoft.Extensions.Caching.Memory;

namespace APICep.Services;



public class CepService:ICepService
{
    private readonly IBrasilApiClient _brasilApiCliente;
    private readonly IMemoryCache _cache;
    private readonly ILogger<CepService> _logger;

    public CepService(IBrasilApiClient brasilApiCliente, IMemoryCache cache, ILogger<CepService> logger)
    {
        _brasilApiCliente = brasilApiCliente;
        _cache = cache;
        _logger = logger;

    }
    public async Task<CepResponse> BuscarCepAsync(string cep)
    {   
        string chaveCache = $"cep_{cep}";

        if (_cache.TryGetValue(chaveCache, out CepResponse? cepCacheado))
        {
            _logger.LogInformation("CACHE HIT - CEP {Cep} recuperado do cache.", cep);
            return cepCacheado!;
        }
       
       _logger.LogInformation("CACHE MISS - CEP {Cep} não encontrado no cache. Consultando API.", cep);

        var cepEncontrado = await _brasilApiCliente.BuscarCepAsync(cep);
        if(cepEncontrado is null)
        {
            throw new CepNaoEncontradoException("CEP não encontrado.");
        }

        var resposta = new CepResponse
        {
            Cep = cepEncontrado.Cep,
            Logradouro = cepEncontrado.Street?? string.Empty,
            Bairro = cepEncontrado.Neighborhood?? string.Empty,
            Cidade = cepEncontrado.City?? string.Empty,
            Estado = cepEncontrado.State?? string.Empty
        };

                var cacheOptions = new MemoryCacheEntryOptions().
                            SetAbsoluteExpiration(TimeSpan.
                            FromMinutes(5));
        _cache.Set(chaveCache, resposta,cacheOptions);
        return resposta;
    }

   
}



