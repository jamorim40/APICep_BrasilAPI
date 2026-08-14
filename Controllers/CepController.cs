
using APICep.Exceptions;
using APICep.Services;
using APICep.Validators;
using Microsoft.AspNetCore.Mvc;

namespace APICep.Controllers;

[ApiController]
[Route("api/cep")]


public class CepController : ControllerBase
{
    private readonly ICepService _cepServico;
    public CepController(ICepService cepServico)
    {
        _cepServico = cepServico;
    }
    [HttpGet("{cep}")]
    public async Task<IActionResult> BuscarCepAsync(string cep)
    {
        cep = CepValidator.Normalizar(cep);

        if (!CepValidator.Validar(cep))
            throw new FormatoIncorretoException("CEP deve conter exatamente 8 dígitos.");
        var cepResposta = await _cepServico.BuscarCepAsync(cep);
        return Ok(cepResposta);

    }
}
