
namespace APICep.DTOs
{
    public class CepResponse
    {
        public string Cep{get;set;} = string.Empty;
        public string Logradouro{get;set;} = string.Empty;
        public string Bairro{get;set;} = string.Empty;
        public string Cidade{get;set;} = string.Empty;
        public string Estado{get;set;} = string.Empty;
    }
}