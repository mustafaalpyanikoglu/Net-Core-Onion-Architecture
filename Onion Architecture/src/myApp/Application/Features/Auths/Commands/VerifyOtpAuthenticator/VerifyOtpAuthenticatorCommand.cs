﻿using Application.Features.Auths.Rules;
using Application.Services.Repositories;
using Application.Services.AuthService;
using Application.Services.UserService;
using Domain.Concrete;
using Entities.Enums;
using MediatR;

namespace Application.Features.Auths.Commands.VerifyOtpAuthenticator;

public class VerifyOtpAuthenticatorCommand : IRequest
{
    public int UserId { get; set; }
    public string ActivationCode { get; set; }

    public class VerifyOtpAuthenticatorCommandHandler : IRequestHandler<VerifyOtpAuthenticatorCommand>
    {
        private readonly AuthBusinessRules _authBusinessRules;
        private readonly IAuthService _authenticatorService;
        private readonly IOtpAuthenticatorRepository _otpAuthenticatorRepository;
        private readonly IUserService _userService;

        public VerifyOtpAuthenticatorCommandHandler(
            AuthBusinessRules authBusinessRules,
            IAuthService authenticatorService,
            IOtpAuthenticatorRepository otpAuthenticatorRepository,
            IUserService userService)
        {
            _authBusinessRules = authBusinessRules;
            _authenticatorService = authenticatorService;
            _otpAuthenticatorRepository = otpAuthenticatorRepository;
            _userService = userService;
        }

        public async Task Handle(VerifyOtpAuthenticatorCommand request, CancellationToken cancellationToken)
        {
            OtpAuthenticator? otpAuthenticator = await _otpAuthenticatorRepository.GetAsync(e => e.UserId == request.UserId);
            await _authBusinessRules.OtpAuthenticatorShouldBeExists(otpAuthenticator);

            User user = await _userService.GetById(request.UserId);

            otpAuthenticator.IsVerified = true;
            user.AuthenticatorType = AuthenticatorType.Otp;

            await _authenticatorService.VerifyAuthenticatorCode(user, request.ActivationCode);

            await _otpAuthenticatorRepository.UpdateAsync(otpAuthenticator);
            await _userService.Update(user);
        }
    }
}
