using APICep.DTOs;

namespace APICep.Clients
{
    public interface IBrasilApiClient
    {
        Task<BrasilCepResponse?> BuscarCep(string cep);
    }
}
