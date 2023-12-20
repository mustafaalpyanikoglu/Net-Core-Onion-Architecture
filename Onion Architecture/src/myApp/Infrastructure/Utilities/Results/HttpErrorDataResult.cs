namespace Infrastructure.Utilities.Results;

public class HttpErrorDataResult<T, K> : HttpDataResult<T,K>
{
    public HttpErrorDataResult(K errorData, string message) : base(errorData, false, message)
    {
    }

    public HttpErrorDataResult(K errorData) : base(errorData, false)
    {
    }
}

