using System.Text.Json.Serialization;

namespace StockExchange.WebAPI.DTOs;

public class RetornoDTO
{
    [JsonPropertyName("resultadoBruto")]
    public decimal? ResultadoBruto { get; set; }

    [JsonPropertyName("resultadoLiquido")]
    public decimal? ResultadoLiquido { get; set; }

    public RetornoDTO()
    {
        this.ResultadoBruto = decimal.Zero;
        this.ResultadoLiquido = decimal.Zero;
    }
}
