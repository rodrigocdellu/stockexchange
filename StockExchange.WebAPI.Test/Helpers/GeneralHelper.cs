using Newtonsoft.Json;
using System.Globalization;

namespace StockExchange.WebAPI.Test.Helpers;

internal sealed class GeneralHelper
{
    private const string DEFAULTSAMPLEDATAPATH = @"Scenarios/RetornoSample.json";

    internal const string MINIMUMMONTHS_MESSAGE = "O parâmetro 'meses' deve ser maior que 1 e menor do que 1201. Valor fornecido: '1'.";

    internal const string MONTHSMAXIMUM_MESSAGE = "O parâmetro 'meses' deve ser maior que 1 e menor do que 1201. Valor fornecido: '1201'.";

    internal const string NEGATIVEINVESTMENT_MESSAGE = "O parâmetro 'valor' deve ser maior que 0.00. Valor fornecido: '-1'.";

    internal const string EXCEPTIONINVESTMENT_MESSAGE = "Exceção forçada para testes.";

    internal const string UNEXPECTEDERROR_MESSAGE = "Unexpected error.";

    internal const string ANOTHEREXCEPTION_MESSAGE = "Another exception was thrown";

    internal static List<RetornoContainerHelper>? LoadData(string sampleDataPath = GeneralHelper.DEFAULTSAMPLEDATAPATH)
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
}
