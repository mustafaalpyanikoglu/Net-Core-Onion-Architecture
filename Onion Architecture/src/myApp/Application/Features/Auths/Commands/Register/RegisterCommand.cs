﻿using Application.Features.Auths.Dtos;
using Application.Features.Auths.Rules;
using Application.Services.Repositories;
using Application.Services.AuthService;
using Core.Security.Hashing;
using Core.Security.Jwt;
using Domain.Concrete;
using Entities.Enums;
using MediatR;
using static Domain.Constants.PathConstant;

namespace Application.Features.Auths.Commands.Register;

public class RegisterCommand : IRequest<RegisteredDto>
{
    public UserForRegisterDto UserForRegisterDto { get; set; }
    public string IPAddress { get; set; }

    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, RegisteredDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly AuthBusinessRules _authBusinessRules;
        private readonly IAuthService _authService;
        private readonly IEmailAuthenticatorRepository _emailAuthenticatorRepository;

        public RegisterCommandHandler(IUserRepository userRepository, AuthBusinessRules authBusinessRules, IAuthService authService, IEmailAuthenticatorRepository emailAuthenticatorRepository)
        {
            _userRepository = userRepository;
            _authBusinessRules = authBusinessRules;
            _authService = authService;
            _emailAuthenticatorRepository = emailAuthenticatorRepository;
        }

        public async Task<RegisteredDto> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            await _authBusinessRules.UserEmailShouldBeNotExists(request.UserForRegisterDto.Email);

            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(request.UserForRegisterDto.Password, out passwordHash, out passwordSalt);


            User newUser = new()
            {
                Email = request.UserForRegisterDto.Email,
                FirstName = request.UserForRegisterDto.FirstName,
                LastName = request.UserForRegisterDto.LastName,
                PhoneNumber = request.UserForRegisterDto.PhoneNumber,
                Address = request.UserForRegisterDto.Address,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                UserStatus = false,
                ImageUrl = DEFAULT_IMAGE_URL,
                RegistrationDate = DateTime.UtcNow,
                AuthenticatorType = AuthenticatorType.Email,
                PasswordResetKey = null,
            };

            User createdUser = await _userRepository.AddAsync(newUser);

            EmailAuthenticator emailAuthenticator = await _authService.CreateEmailAuthenticator(createdUser);
            await _emailAuthenticatorRepository.AddAsync(emailAuthenticator);

            AccessToken createdAccessToken = await _authService.CreateAccessToken(createdUser);

            RefreshToken createdRefreshToken = await _authService.CreateRefreshToken(createdUser, request.IPAddress);

            RefreshToken addedRefreshToken = await _authService.AddRefreshToken(createdRefreshToken);

            RegisteredDto registeredDto = new()
            {
                AccessToken = createdAccessToken,
                RefreshToken = addedRefreshToken
            };

            return registeredDto;
        }
    }
}