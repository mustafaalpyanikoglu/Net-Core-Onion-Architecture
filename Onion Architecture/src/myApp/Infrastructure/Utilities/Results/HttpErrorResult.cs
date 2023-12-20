namespace Infrastructure.Utilities.Results;

public class HttpErrorResult : HttpResult
{
    public HttpErrorResult(string message) : base(false, message)
    {
    }

    public HttpErrorResult() : base(false)
    {
    }
}

