using StockExchange.WebAPI.DTOs;
using StockExchange.WebAPI.Helpers;

namespace StockExchange.WebAPI.Services;

public interface ICdbService
{
    Task<ServiceResultHelper<RetornoDto>> SolicitarCalculoInvestimento(InvestimentoDto investimento);
}
