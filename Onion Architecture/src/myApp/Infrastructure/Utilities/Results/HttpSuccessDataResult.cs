namespace Infrastructure.Utilities.Results;

public class HttpSuccessDataResult<T, K> : HttpDataResult<T, K>
{
    public HttpSuccessDataResult(T successData, string message) : base(successData, true, message)
    {
    }

    public HttpSuccessDataResult(T successData) : base(successData, true)
    {
    }
}

