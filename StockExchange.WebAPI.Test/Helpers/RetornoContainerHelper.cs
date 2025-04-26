using Newtonsoft.Json;

namespace StockExchange.WebAPI.Test.Helpers
{
    public class RetornoContainerHelper
    {
        [JsonProperty("retornosValidos")]
        public List<RetornoHelper> RetornosValidos { get; set; }

        [JsonProperty("retornosInvalidos")]
        public List<RetornoHelper> RetornosInvalidos { get; set; }

        public RetornoContainerHelper()
        {
            this.RetornosValidos = new List<RetornoHelper>();
            this.RetornosInvalidos = new List<RetornoHelper>();
        }
    }
}
