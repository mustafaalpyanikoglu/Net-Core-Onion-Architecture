namespace Infrastructure.Utilities.HttpClientGenericRepository;

public interface IHttpClientRepository
{
    Task<string> SendSoapRequest(string url, HttpMethod httpMethod, string requestSoapData, string? token);
}
