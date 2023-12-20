
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Text;
using static Infrastructure.Constants.HttpClientConstants;

namespace Infrastructure.Utilities.HttpClientGenericRepository;

/// <summary>
/// Helper class for sending HTTP requests and handling responses.
/// </summary>
public class HttpClientRepository:IHttpClientRepository
{
    private readonly IHttpClientFactory httpClientFactory;

    public HttpClientRepository(IHttpClientFactory httpClientFactory)
    {
        this.httpClientFactory = httpClientFactory;
    }

    public async Task<string> SendSoapRequest(
        string url, HttpMethod httpMethod, string requestSoapData, string? token)
    {
        using (HttpClient client = httpClientFactory.CreateClient())
        {
            client.DefaultRequestHeaders.Accept.Clear();
            //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(MediaTypeNames.Application.Soap));

            if (token is not null) client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(BEARER, token);

            using (HttpRequestMessage request = new HttpRequestMessage(httpMethod, url))
            {
                request.Content = new StringContent(requestSoapData, Encoding.UTF8, MediaTypeNames.Application.Soap);
                using (HttpResponseMessage response = await client.SendAsync(request))
                {
                    using (HttpContent content = response.Content)
                    {
                        string result = await content.ReadAsStringAsync();
                        return result;
                    }
                }
            }
        }
    }

}