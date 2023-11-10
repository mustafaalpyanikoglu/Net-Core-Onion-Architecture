using Core.Application.Dtos;

namespace Application.Features.Auths.Dtos;

public class ForgotPasswordEmailAuthenticatorResponseDto : IDto
{
    public int UserId { get; set; }
    public string PasswordResetKey { get; set; }
}
