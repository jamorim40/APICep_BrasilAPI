

namespace APICep.Validators;

public static class CepValidator
{
    public static string Normalizar(string cep)
    {
        return cep
            .Trim()
            .Replace("-", "")
            .Replace(" ", "");
    }
    public static bool Validador(string cep)
    {
        //cep = Normalizar(cep);

        if (string.IsNullOrWhiteSpace(cep))
            return false;
        if (cep.Length != 8)
            return false;
        if (!cep.All(char.IsDigit))
            return false;

        return true;
    }
}

   
