using System.Text.Json.Serialization;

namespace StockExchange.WebAPI.DTOs;

public class InvestimentoDto
{
    [JsonPropertyName("valor")]
    public decimal? Valor { get; set; }

    [JsonPropertyName("meses")]
    public uint? Meses { get; set; }

    public InvestimentoDto()
    {
        this.Valor = decimal.Zero;
        this.Meses = 0U;
    }
}
