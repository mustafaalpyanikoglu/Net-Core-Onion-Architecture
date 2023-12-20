namespace Infrastructure.Utilities.Results;

public class HttpDataResult<T, K> : HttpResult, IHttpDataResult<T, K>
{
    public HttpDataResult(T successData, bool success, string message) : base(success, message)
    {
        this.SuccessData = successData;
        this.ErrorData = default!;
    }
    public HttpDataResult(T successData, bool success) : base(success)
    {
        this.SuccessData = successData;
        this.ErrorData = default!;
    }

    public HttpDataResult(K errorData, bool success, string message) : base(success, message)
    {
        this.SuccessData = default!;
        this.ErrorData = errorData;
    }

    public HttpDataResult(K errorData, bool success) : base(success)
    {
        this.SuccessData = default!;
        this.ErrorData = errorData;
    }

    public T SuccessData { get; }

    public K ErrorData { get; }
}

