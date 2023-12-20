namespace Infrastructure.Utilities.Results;

public class HttpSuccessResult<K> : HttpResult
{
    public HttpSuccessResult(string message) : base(true, message)
    {
    }

    public HttpSuccessResult() : base(true)
    {
    }
}

