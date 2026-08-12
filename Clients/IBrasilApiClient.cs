using APICep.DTOs;

namespace APICep.Clients
{
    public interface IBrasilApiClient
    {
        Task<BrasilCepResponse?> BuscarCepAsync(string cep);
    }
}
