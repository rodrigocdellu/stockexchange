using System.Globalization;
using System.Text.RegularExpressions;
using FluentValidation;
using StockExchange.WebAPI.DTOs;

namespace StockExchange.WebAPI.Validators;

public class InvestimentoValidator : AbstractValidator<InvestimentoDto>
{
    private const string REGEX = @"^[+-]?\d+([.,]\d+)?$"; // Regex para decimal com ponto ou vírgula

    public const decimal VALOR_GREATERTHAN = 0.00m;

    public const uint MESES_INCLUSIVEBETWEEN_MINIMUM = 1U;

    public const uint MESES_INCLUSIVEBETWEEN_MAXIMUM = 1200U;

    #region Mensagens de Validação para Valor

    private const string? VALOR_NOTNULL_MESSAGE = @"O parâmetro 'valor' não pode ser nulo. Valor fornecido: '{PropertyValue}'.";

    private const string? VALOR_NOTEMPTY_MESSAGE = @"O parâmetro 'valor' não pode estar vazio. Valor fornecido: '{PropertyValue}'.";

    private const string? VALOR_FORMAT_MESSAGE = @"O parâmetro 'valor' deve ser do formato decimal. Valor fornecido: '{PropertyValue}'.";

    private const string? VALOR_TYPE_MESSAGE = @"O parâmetro 'valor' deve ser do tipo decimal. Valor fornecido: '{PropertyValue}'.";

    private const string? VALOR_GREATERTHAN_MESSAGE = @"O parâmetro 'valor' deve ser maior que 0.00. Valor fornecido: '{PropertyValue}'.";

    #endregion

    #region Mensagens de Validação para Meses

    private const string? MESES_NOTNULL_MESSAGE = @"O parâmetro 'meses' não pode ser nulo. Valor fornecido: '{PropertyValue}'.";

    private const string? MESES_NOTEMPTY_MESSAGE = @"O parâmetro 'meses' não pode estar vazio. Valor fornecido: '{PropertyValue}'.";

    private const string? MESES_TYPE_MESSAGE = @"O parâmetro 'meses' deve ser do tipo uint. Valor fornecido: '{PropertyValue}'.";

    private const string? MESES_INCLUSIVEBETWEEN_MESSAGE = @"O parâmetro 'meses' deve ser maior que 0 e menor do que 1201. Valor fornecido: '{PropertyValue}'.";

    #endregion

    public InvestimentoValidator()
    {
        RuleFor(investimento => investimento.Valor)
            .NotNull().WithMessage(InvestimentoValidator.VALOR_NOTNULL_MESSAGE)
            .NotEmpty().WithMessage(InvestimentoValidator.VALOR_NOTEMPTY_MESSAGE)
            .Must(valor => Regex.IsMatch(Convert.ToString(valor, CultureInfo.InvariantCulture) ?? String.Empty, InvestimentoValidator.REGEX, RegexOptions.None, TimeSpan.FromMilliseconds(1000))).WithMessage(InvestimentoValidator.VALOR_FORMAT_MESSAGE)
            .Must(valor => decimal.TryParse(valor.ToString(), NumberStyles.Any, CultureInfo.InvariantCulture, out _)).WithMessage(InvestimentoValidator.VALOR_TYPE_MESSAGE)
            .GreaterThan(InvestimentoValidator.VALOR_GREATERTHAN).WithMessage(InvestimentoValidator.VALOR_GREATERTHAN_MESSAGE);

        RuleFor(investimento => investimento.Meses)
            .NotNull().WithMessage(InvestimentoValidator.MESES_NOTNULL_MESSAGE)
            .NotEmpty().WithMessage(InvestimentoValidator.MESES_NOTEMPTY_MESSAGE)
            .Must(valor => uint.TryParse(valor.ToString(), NumberStyles.Any, CultureInfo.InvariantCulture, out _)).WithMessage(InvestimentoValidator.MESES_TYPE_MESSAGE)
            .InclusiveBetween(InvestimentoValidator.MESES_INCLUSIVEBETWEEN_MINIMUM, InvestimentoValidator.MESES_INCLUSIVEBETWEEN_MAXIMUM).WithMessage(InvestimentoValidator.MESES_INCLUSIVEBETWEEN_MESSAGE);
    }
}
