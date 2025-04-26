using System.Net;
using Microsoft.AspNetCore.Mvc;
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
    public async Task<IActionResult> SolicitarCalculoInvestimento(decimal investimento, uint meses)
    {
        try
        {
            // Await the service result
            var result = await this._CDBService.SolicitarCalculoInvestimento(investimento, meses);

            // Validate the result
            if (!result.Success)
                return BadRequest(new { error = result.ErrorMessage });

            // Return the result
            return Ok(result.Data);
        }
        catch (Exception exception)
        {
            // Return error result
            return StatusCode(Convert.ToInt32(HttpStatusCode.InternalServerError), exception);
        }
    }     
}
