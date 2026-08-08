using APICep.DTOs;

namespace APICep.Services
{
    public interface ICepService
    {
        Task<CepResponse> BuscarCep(string cep);
    }
}
