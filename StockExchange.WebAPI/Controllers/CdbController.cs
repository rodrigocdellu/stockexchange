using System.Net;
using Microsoft.AspNetCore.Mvc;
using StockExchange.WebAPI.DTOs;
using StockExchange.WebAPI.Services;

namespace StockExchange.WebAPI.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public sealed class CdbController : ControllerBase
{
    private readonly ICdbService _CDBService;
    
    public CdbController(ICdbService cdbService)
    {
        this._CDBService = cdbService;
    }
    
    [HttpGet("SolicitarCalculoInvestimento")]
    public async Task<IActionResult> SolicitarCalculoInvestimento([FromQuery]InvestimentoDTO investimento)
    {
        try
        {
            // Validação adicional contra abuso
            if (investimento.Meses < 1 || investimento.Meses > 360)
            {
                return BadRequest(new { error = "O número de meses deve estar entre 1 e 360." });
            }

            // Check Fluent Validation
            if (ModelState.IsValid)
            {
                // Await the service result
                var result = await this._CDBService.SolicitarCalculoInvestimento(Convert.ToDecimal(investimento.Valor), Convert.ToUInt32(investimento.Meses));

                // Validate the result
                if (!result.Success)
                    return BadRequest(new { error = result.ErrorMessage });

                // Return the result
                return Ok(result.Data);
            }
            else
                // Return Fluent Validation erros
                return BadRequest(ModelState);            
        }
        catch (Exception exception)
        {
            // Return error result
            return StatusCode(Convert.ToInt32(HttpStatusCode.InternalServerError), exception);
        }
    }     
}
