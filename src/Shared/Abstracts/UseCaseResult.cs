using System.Text.Json.Serialization;

namespace Shared.Abstracts;

public sealed class UseCaseResult<TData>
{
    [JsonPropertyName("sucesso")]
    public bool Success { get; private set; }
    [JsonPropertyName("mensagem")]
    public string Message { get; private set; } = string.Empty;
    [JsonPropertyName("status")]
    public int StatusCode { get; private set; }
    [JsonPropertyName("dados")]
    public TData? Data { get; private set; }

    public UseCaseResult<TData> Ok(string message = "Success", TData? data = default)
    {
        Success = true;
        Message = message;
        StatusCode = 200;
        Data = data;
        return this;
    }

    public UseCaseResult<TData> Created(string message, TData? data = default)
    {
        Success = true;
        Message = message;
        StatusCode = 201;
        Data = data;
        return this;
    }

    public UseCaseResult<TData> Fail(string message)
    {
        Success = false;
        Message = message;
        StatusCode = 400;
        return this;
    }

    public UseCaseResult<TData> Unauthorized(string message = "Unauthorized")
        => new() { Success = false, Message = message, StatusCode = 401 };

    public UseCaseResult<TData> InternalError(string message = "Internal Server Error")
        => new() { Success = false, Message = message, StatusCode = 500 };

    public UseCaseResult<TData> Forbidden(string message = "Forbidden")
        => new() { Success = false, Message = message, StatusCode = 403 };

    public UseCaseResult<TData> NotFound(string message = "Not Found")
        => new() { Success = false, Message = message, StatusCode = 404 };
}
