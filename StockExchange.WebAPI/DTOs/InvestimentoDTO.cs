using System.Text.Json.Serialization;

namespace StockExchange.WebAPI.DTOs;

public class InvestimentoDTO
{
    [JsonPropertyName("valor")]
    public decimal? Valor { get; set; }

    [JsonPropertyName("meses")]
    public uint? Meses { get; set; }

    public InvestimentoDTO()
    {
        this.Valor = decimal.Zero;
        this.Meses = 0U;
    }
}
