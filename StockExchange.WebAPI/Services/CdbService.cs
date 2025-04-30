using StockExchange.WebAPI.Helpers;
using StockExchange.WebAPI.DTOs;

namespace StockExchange.WebAPI.Services;

public class CdbService : ICdbService
{
    public RetornoDTO Retorno { get; set; }
    
    public CdbService()
    {
        this.Retorno = new RetornoDTO();
    }    

    private static decimal ObterAliquotaImposto(uint prazoMeses)
    {
        // Até 06 meses: 22,5%
        if (prazoMeses <= 6)
            // 22,5% = (22,5m / 100)
            return 0.225m;

        // Até 12 meses: 20%
        if (prazoMeses <= 12)
            // 20% = (20m / 100)
            return 0.20m;
        
        // Até 24 meses 17,5%
        if (prazoMeses <= 24)
            // 17,5% = (17,5m / 100)
            return 0.175m;
        
        // Acima de 24 meses 15%
        // 15% = (15m / 100)
        return 0.15m;      
    }    

    /// <summary>
    /// Para o cálculo do CDB, deve-se utilizar a fórmula VF = VI x [1 + (CDI x TB)] onde:
    ///     i. VF é o valor final;
    ///     ii. VI é o valor inicial;
    ///     iii. CDI é o valor dessa taxa no último mês;
    ///     iv. TB é quanto o banco paga sobre o CDI;
    ///     Nota: A fórmula calcula somente o valor de um mês. Ou seja, os rendimentos de cada mês devem ser utilizados para calcular o mês seguinte .
    ///
    /// Para medida do Exercício considerar os valores abaixo como fixos:
    ///     i. TB – 108%
    ///     ii. CDI – 0,9%
    ///
    /// Para cálculo do imposto utilizar a seguinte tabela:
    ///     i. Até 06 meses: 22,5%
    ///     ii. Até 12 meses: 20%
    ///     iii. Até 24 meses 17,5%
    ///     iv. Acima de 24 meses 15%
    /// </summary>
    /// <param name="investimento">Valor inicial de investimento em R$ (Real Brasil)</param>
    /// <param name="meses">Prazo de investimento em meses</param>
    private void Calcula(decimal investimento, uint meses)
    {
        // Inicializando as variáveis
        var VI = investimento;
        var CDI = 0.009m; // 0,9% = (0,9m / 100)
        var TB = 1.08m; // 108% = (108m / 100)
        var VF = VI; // Investimento mínimo
        var lucro = 0m;
        var imposto = 0m;

        // Cálculo dos juros compostos mês a mês
        for (uint mes = 0; mes < meses; mes++)
            VF *= (1 + (CDI * TB));

        // Cálculo do imposto sobre o lucro
        lucro = VF - VI;
        imposto = lucro * CdbService.ObterAliquotaImposto(meses);

        // Prepara o objeto investimento
        this.Retorno.ResultadoBruto = investimento + lucro;
        this.Retorno.ResultadoLiquido = investimento + lucro - imposto;
    }

    public Task<ServiceResultHelper<RetornoDTO>> SolicitarCalculoInvestimento(decimal investimento, uint meses)
    {
        try
        {
            // Realiza os cálculos de investimento
            this.Calcula(investimento, meses);

            // Retorna o investimento
            return Task.FromResult(ServiceResultHelper<RetornoDTO>.Ok(this.Retorno));
        }
        catch (Exception exception)
        {
            // Retorna o exceção para os cálculos de investimento
            return Task.FromResult(ServiceResultHelper<RetornoDTO>.Fail(exception.Message));
        }
    }
}
