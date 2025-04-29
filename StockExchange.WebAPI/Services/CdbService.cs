using System.Globalization;
using System.Text.RegularExpressions;
using StockExchange.WebAPI.Helpers;
using StockExchange.WebAPI.Models;

namespace StockExchange.WebAPI.Services;

public class CdbService : ICdbService
{
    public Retorno Retorno { get; set; }
    
    public CdbService()
    {
        this.Retorno = new Retorno();
    }

    private static bool ValidaEstrutura(ref uint meses, ref decimal investimento, out string? mensagemValidacao)
    {
        var investimentoValidado = 0m; // Define variável para testar o tipo do parâmetro investimento
        var mesesValidado = 0U; // Define variável para testar o tipo do parâmetro meses
        var regex = @"^[+-]?\d+([.,]\d+)?$"; // Regex para decimal com ponto ou vírgula
        var estaValido = false; // O estado inicial é inválido

        // A mensagem de validação inicial é vazia
        mensagemValidacao = String.Empty;
        
        #region Validação de Formato
        
        // Verifica o formato do parâmetro investimento
        estaValido = Regex.IsMatch(investimento.ToString(CultureInfo.InvariantCulture), regex, RegexOptions.None, TimeSpan.FromMilliseconds(1000));
        
        // Valida o formato e define a mensagem
        if (!estaValido)
        {
            mensagemValidacao = $"O parâmetro 'investimento' possui um formato inválido. Valor fornecido: '{investimento}'";

            return estaValido;
        }

        #endregion

        #region Validação de Tipo

        // Verifica o tipo do parâmetro investimento
        estaValido = decimal.TryParse(investimento.ToString(), NumberStyles.Any, CultureInfo.InvariantCulture, out investimentoValidado);

        // Valida o tipo e define a mensagem
        if (!estaValido)
        {
            mensagemValidacao = $"O parâmetro 'investimento' possui um tipo inválido. Valor fornecido: '{investimento}'";

            return estaValido;
        }

        // Verifica o tipo do parâmetro investimento
        estaValido = uint.TryParse(meses.ToString(), NumberStyles.Any, CultureInfo.InvariantCulture, out mesesValidado);

        // Valida o tipo e define a mensagem
        if (!estaValido)
        {
            mensagemValidacao = $"O parâmetro 'meses' possui um tipo inválido. Valor fornecido: '{meses}'";

            return estaValido;
        }

        #endregion

        // Atualiza as referências com os valores validados
        meses = mesesValidado;
        investimento = investimentoValidado;

        return estaValido;
    }

    private static bool ValidaNegocio(ref uint meses, ref decimal investimento, out string? mensagemValidacao)
    {
        // A mensagem de validação inicial é vazia
        mensagemValidacao = String.Empty;

        // Valida o valor e define a mensagem
        if (investimento <= 0)
        {
            mensagemValidacao = $"O parâmetro 'investimento' não pode ser negativo. Valor fornecido: '{investimento}'";

            return false;
        }
        
        // Valida o valor e define a mensagem
        if (meses < 1)
        {
            mensagemValidacao = $"O parâmetro 'meses' deve ser maior que zero. Valor fornecido: '{meses}'";

            return false;
        }

        return true;
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
        this.Retorno.ResultadoBruto = lucro;
        this.Retorno.ResultadoLiquido = lucro - imposto;
    }

    public Task<ServiceResultHelper<Retorno>> SolicitarCalculoInvestimento(decimal investimento, uint meses)
    {
        // Define a mensagem de validação
        string? mensagemValidacao;

        // Aplica regras de validação estrutural
        if (CdbService.ValidaEstrutura(ref meses, ref investimento, out mensagemValidacao))
        {
            // Aplica regras de validação do negócio
            if (CdbService.ValidaNegocio(ref meses, ref investimento, out mensagemValidacao))
            {
                try
                {
                    // Realiza os cálculos de investimento
                    this.Calcula(investimento, meses);
                }
                catch (Exception exception)
                {
                    // Retorna o exceção para os cálculos de investimento
                    return Task.FromResult(ServiceResultHelper<Retorno>.Fail(exception.Message));
                }
        
                // Retorna o investimento
                return Task.FromResult(ServiceResultHelper<Retorno>.Ok(this.Retorno));
            }
            else
                // Retorna o exceção para validação estrutural
                return Task.FromResult(ServiceResultHelper<Retorno>.Fail(mensagemValidacao));
        }
        else
            // Retorna o exceção para validação estrutural
            return Task.FromResult(ServiceResultHelper<Retorno>.Fail(mensagemValidacao));
    }
}
