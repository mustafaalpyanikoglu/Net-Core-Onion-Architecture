using Core.Application.Dtos;

namespace Application.Features.Auths.Dtos;

public class EnableEmailAuthenticatorResponseDto : IDto
{
    public int UserId { get; set; }
    public string ActivationKey { get; set; }
}
