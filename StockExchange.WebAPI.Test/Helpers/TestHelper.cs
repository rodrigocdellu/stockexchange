using System.Globalization;
using Newtonsoft.Json;

namespace StockExchange.WebAPI.Test.Helpers
{
    internal sealed class TestHelper
    {
        private const string DEFAULTSAMPLEDATAPATH = @"Data/RetornoSample.json";

        internal static List<RetornoContainerHelper>? LoadData(string sampleDataPath = TestHelper.DEFAULTSAMPLEDATAPATH)
        {
            try
            {
                // Set the variables
                string? jsonString = null;

                // Read all sample data
                jsonString = File.ReadAllText(sampleDataPath);

                if (String.IsNullOrWhiteSpace(jsonString))
                    return new List<RetornoContainerHelper>();
                else
                {
                    // Load sample data
                    var jsonObject = JsonConvert.DeserializeObject<List<RetornoContainerHelper>>(jsonString);

                    // Check the json object
                    if (jsonObject != null)
                        return jsonObject;
                    else
                        return new List<RetornoContainerHelper>();
                }
            }
            catch (Exception)
            {
                throw;
            }            
        }

        internal static void CastData(RetornoHelper retornoValido, out decimal investimento, out uint meses, out decimal resultadoBruto, out decimal resultadoLiquido)
        {
            if (!decimal.TryParse(retornoValido.Investimento, NumberStyles.Any, CultureInfo.InvariantCulture, out investimento))
                throw new InvalidCastException($"O valor fornecido não é válido. Por favor, corrija o paramêtro 'Investimento': '{retornoValido.Investimento}.");

            if (!uint.TryParse(retornoValido.Meses, NumberStyles.Any, CultureInfo.InvariantCulture, out meses))
                throw new InvalidCastException($"O valor fornecido não é válido. Por favor, corrija o paramêtro 'Meses': '{retornoValido.Meses}.");

            if (!decimal.TryParse(retornoValido.ResultadoBruto, NumberStyles.Any, CultureInfo.InvariantCulture, out resultadoBruto))
                throw new InvalidCastException($"O valor fornecido não é válido. Por favor, corrija o paramêtro 'ResultadoBruto': '{retornoValido.ResultadoBruto}.");

            if (!decimal.TryParse(retornoValido.ResultadoLiquido, NumberStyles.Any, CultureInfo.InvariantCulture, out resultadoLiquido))
                throw new InvalidCastException($"O valor fornecido não é válido. Por favor, corrija o paramêtro 'ResultadoLiquido': '{retornoValido.ResultadoLiquido}.");
        }

        internal static TimeZoneInfo? GetBrasilianTimeZone()
        {
            // Looking for a Brazil/SãoPaulo timezone due to Docker Container timezone
            return TimeZoneInfo.GetSystemTimeZones()
                .FirstOrDefault(internalTimeZone =>
                    (internalTimeZone.Id.Contains("Brazil", StringComparison.OrdinalIgnoreCase) ||
                    internalTimeZone.Id.Contains("E. South America", StringComparison.OrdinalIgnoreCase) ||
                    internalTimeZone.Id.Contains("America/Sao_Paulo", StringComparison.OrdinalIgnoreCase) ||
                    internalTimeZone.DisplayName.Contains("Brazil", StringComparison.OrdinalIgnoreCase) ||
                    internalTimeZone.DisplayName.Contains("Sao Paulo", StringComparison.OrdinalIgnoreCase)) &&
                    internalTimeZone.GetUtcOffset(DateTime.Now) == TimeSpan.FromHours(-3)
                );
        }
    }
}
