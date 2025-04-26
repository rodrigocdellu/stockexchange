using StockExchange.WebAPI.Helpers;
using StockExchange.WebAPI.Models;

namespace StockExchange.WebAPI.Services;

public interface ICdbService
{
    Task<ServiceResultHelper<Retorno>> SolicitarCalculoInvestimento(decimal investimento, uint meses);
}
