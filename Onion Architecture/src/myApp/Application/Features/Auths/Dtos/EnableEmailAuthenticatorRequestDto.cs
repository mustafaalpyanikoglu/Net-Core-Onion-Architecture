using Core.Application.Dtos;

namespace Application.Features.Auths.Dtos;

public class EnableEmailAuthenticatorRequestDto : IDto
{
    public string Email { get; set; }
}
