using System.Text.Json.Serialization;

namespace StockExchange.WebAPI.Models;

public class Retorno
{
    [JsonPropertyName("resultadoBruto")]
    public decimal? ResultadoBruto { get; set; }

    [JsonPropertyName("resultadoLiquido")]
    public decimal? ResultadoLiquido { get; set; }

    public Retorno()
    {
        this.ResultadoBruto = decimal.Zero;
        this.ResultadoLiquido = decimal.Zero;
    }
}
