
using APICep.Exceptions;
using APICep.Services;
using APICep.Validators;
using Microsoft.AspNetCore.Mvc;

namespace APICep.Controllers;

[ApiController]
[Route("api/cep")]


public class CepController : ControllerBase
{
    private readonly ICepService _cepService;
    public CepController(ICepService cepService)
    {
        _cepService = cepService;
    }
    [HttpGet("{cep}")]
    public async Task<IActionResult> BuscarCepAsync(string cep)
    {
        cep = CepValidator.Normalizar(cep);

        if (!CepValidator.Validar(cep))
            throw new FormatoIncorretoException("CEP deve conter exatamente 8 dígitos.");
        var cepResponser = await _cepService.BuscarCepAsync(cep);
        return Ok(cepResponser);

    }
}
