using System.Text.Json.Serialization;

namespace StockExchange.WebAPI.DTOs;

public class RetornoDto
{
    [JsonPropertyName("resultadoBruto")]
    public decimal? ResultadoBruto { get; set; }

    [JsonPropertyName("resultadoLiquido")]
    public decimal? ResultadoLiquido { get; set; }

    public RetornoDto()
    {
        this.ResultadoBruto = decimal.Zero;
        this.ResultadoLiquido = decimal.Zero;
    }
}
