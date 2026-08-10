
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
    public async Task<IActionResult> BuscarCep(string cep)
    {
        cep = CepValidator.Normalizar(cep);

        if (!CepValidator.Validar(cep))
            throw new FormatoIncorretoException("CEP deve conter exatamente 8 dígitos.");
        var resultadoBuscar = await _cepService.BuscarCep(cep);
        return Ok(resultadoBuscar);

    }
}
