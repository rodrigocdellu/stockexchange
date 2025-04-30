using System.Net;
using Microsoft.AspNetCore.Mvc;
using StockExchange.WebAPI.Validators;
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
            // Check Fluent Validation
            if (investimento.Meses > InvestimentoValidator.MESES_INCLUSIVEBETWEEN_MINIMUM - 1 && investimento.Meses < InvestimentoValidator.MESES_INCLUSIVEBETWEEN_MAXIMUM + 1 && // Added by SonarQube security issue
                ModelState.IsValid)
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
