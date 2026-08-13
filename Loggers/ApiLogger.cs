namespace APICep.Loggers
{
    public class ApiLogger
    {
        public ApiLogger(int statusCode, string message)
        {
            StatusCodigo = statusCode.ToString();
            Mensagem = message;
            
        }

        public string? StatusCodigo{get;set;}
        public string? Mensagem{get;set;}
        
    }
}