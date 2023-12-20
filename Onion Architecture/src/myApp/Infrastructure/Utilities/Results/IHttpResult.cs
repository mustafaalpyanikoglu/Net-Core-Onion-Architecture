namespace Infrastructure.Utilities.Results;

public interface IHttpResult
{
    string Message { get; }
    bool Success { get; }
}