namespace StockExchange.WebAPI.Helpers;

public sealed class ServiceResultHelper<T>
{
    public bool Success { get; set; }

    public string? ErrorMessage { get; set; }

    public T? Data { get; set; }

    public static ServiceResultHelper<T> Ok(T? data) => new() { Success = true, Data = data };

    public static ServiceResultHelper<T> Fail(string? errorMessage) => new() { Success = false, ErrorMessage = errorMessage };
}
