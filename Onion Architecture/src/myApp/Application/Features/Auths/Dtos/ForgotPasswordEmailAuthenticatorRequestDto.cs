using Core.Application.Dtos;

namespace Application.Features.Auths.Dtos;

public class ForgotPasswordEmailAuthenticatorRequestDto : IDto
{
    public string Email { get; set; }
}
