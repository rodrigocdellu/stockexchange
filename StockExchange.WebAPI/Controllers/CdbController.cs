using System.Net;
using Microsoft.AspNetCore.Mvc;
using StockExchange.WebAPI.DTOs;
using StockExchange.WebAPI.Services;
using StockExchange.WebAPI.Validators;

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
    public async Task<IActionResult> SolicitarCalculoInvestimento([FromQuery]InvestimentoDto investimento)
    {
        try
        {
            // Check Fluent Validation
            if (investimento.Meses > InvestimentoValidator.MESES_INCLUSIVEBETWEEN_MINIMUM - 1 && investimento.Meses < InvestimentoValidator.MESES_INCLUSIVEBETWEEN_MAXIMUM + 1 && // Added by SonarQube security issue
                ModelState.IsValid)
            {
                // Await the service result
                var result = await this._CDBService.SolicitarCalculoInvestimento(investimento);

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
