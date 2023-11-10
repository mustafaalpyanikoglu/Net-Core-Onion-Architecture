using Core.Application.Responses;

namespace Application.Features.Auths.Commands.EnableOtpAuthenticator;

public class EnabledOtpAuthenticatorResponse : IResponse
{
    public string SecretKey { get; set; }
}
