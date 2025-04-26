using System.Text.Json.Serialization;

namespace StockExchange.WebAPI.Test.Helpers
{
    public class RetornoHelper
    {
        [JsonPropertyName("investimento")]
        public string? Investimento { get; set; }

        [JsonPropertyName("meses")]
        public string? Meses { get; set; }


        [JsonPropertyName("resultadoBruto")]
        public string? ResultadoBruto { get; set; }

        [JsonPropertyName("resultadoLiquido")]
        public string? ResultadoLiquido { get; set; }

        public RetornoHelper()
        {
            this.Investimento = decimal.Zero.ToString();
            this.Meses = uint.MinValue.ToString();
            this.ResultadoBruto = decimal.Zero.ToString();
            this.ResultadoLiquido = decimal.Zero.ToString();
        }
    }
}
