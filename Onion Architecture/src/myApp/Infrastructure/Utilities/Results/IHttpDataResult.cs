namespace Infrastructure.Utilities.Results;

public interface IHttpDataResult<T, K> : IHttpResult
{
    T SuccessData { get; }
    K ErrorData { get; }
}

