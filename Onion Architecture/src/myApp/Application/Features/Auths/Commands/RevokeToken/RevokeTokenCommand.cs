using Application.Features.Auths.Dtos;
using Application.Features.Auths.Rules;
using AutoMapper;
using Application.Services.AuthService;
using Domain.Concrete;
using MediatR;

namespace Application.Features.Auths.Commands.RevokeToken;

public class RevokeTokenCommand : IRequest<RevokedTokenDto>
{
    public string Token { get; set; }
    public string IPAddress { get; set; }

    public class RevokeTokenCommandHandler : IRequestHandler<RevokeTokenCommand, RevokedTokenDto>
    {
        private readonly IAuthService _authService;
        private readonly AuthBusinessRules _authBusinessRules;
        private readonly IMapper _mapper;

        public RevokeTokenCommandHandler(IAuthService authService, AuthBusinessRules authBusinessRules, IMapper mapper)
        {
            _authService = authService;
            _authBusinessRules = authBusinessRules;
            _mapper = mapper;
        }

        public async Task<RevokedTokenDto> Handle(RevokeTokenCommand request, CancellationToken cancellationToken)
        {
            RefreshToken? refreshToken = await _authService.GetRefreshTokenByToken(request.Token);

            // Check if the refresh token exists
            await _authBusinessRules.RefreshTokenShouldBeExists(refreshToken);

            // Check if the refresh token is active
            await _authBusinessRules.RefreshTokenShouldBeActive(refreshToken);

            // Revoke the refresh token without replacement
            await _authService.RevokeRefreshToken(refreshToken, request.IPAddress, "Revoked without replacement");

            // Map the revoked refresh token to a RevokedTokenDto
            RevokedTokenDto revokedTokenDto = _mapper.Map<RevokedTokenDto>(refreshToken);

            return revokedTokenDto;
        }
    }
}