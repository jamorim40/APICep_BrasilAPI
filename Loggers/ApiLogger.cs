namespace APICep.Loggers
{
    public class ApiLogger
    {
        public ApiLogger(int codigoStatus, string mensagem)
        {
            StatusCodigo = codigoStatus.ToString();
            Mensagem = mensagem;
        }

        public string? StatusCodigo{get;set;}
        public string? Mensagem{get;set;}
        
    }
}