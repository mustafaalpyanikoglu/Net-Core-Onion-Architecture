namespace Infrastructure.Utilities.Results;

public class HttpResult : IHttpResult
{
    public HttpResult(bool success, string message) : this(success)
    {
        this.Message = message;
    }

    public HttpResult(bool success)
    {
        this.Success = success;
        this.Message = string.Empty;
    }

    public bool Success { get; }

    public string Message { get; }
}


