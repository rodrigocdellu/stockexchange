using StockExchange.WebAPI.Helpers;
using StockExchange.WebAPI.DTOs;

namespace StockExchange.WebAPI.Services;

public interface ICdbService
{
    Task<ServiceResultHelper<RetornoDTO>> SolicitarCalculoInvestimento(decimal investimento, uint meses);
}
