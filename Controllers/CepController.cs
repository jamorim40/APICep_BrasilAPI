
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

        if (!CepValidator.Validador(cep))
            return BadRequest("CEP deve conter exatamento 8 dígitos.");

        try
        {

            var resultadoBusca = await _cepService.BuscarCep(cep);

            return Ok(resultadoBusca);
        }
        catch(CepNaoEncontradoException ex)
        {
            return NotFound(ex.Message);
        }
         catch(Exception)
        {
            return StatusCode(500, "Erro interno no serviço CEP.");
        } 
    }
}
