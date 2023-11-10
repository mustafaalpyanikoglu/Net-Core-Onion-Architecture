using Application.Features.Auths.Dtos;
using Application.Features.Auths.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Application.Services.AuthService;
using Application.Services.UserService;
using Domain.Concrete;
using MediatR;

namespace Application.Features.Auths.Commands.ForgotPasswordEmailAuthenticator;

public class ForgotPasswordEmailAuthenticatorCommand : IRequest<ForgotPasswordEmailAuthenticatorResponseDto>
{
    public string Email { get; set; }
    public string VerifyEmailUrlPrefix { get; set; }

    public class ForgotPasswordEmailAuthenticatorCommandHandler : IRequestHandler<ForgotPasswordEmailAuthenticatorCommand, ForgotPasswordEmailAuthenticatorResponseDto>
    {
        private readonly AuthBusinessRules _authBusinessRules;
        private readonly IAuthService _authenticatorService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public ForgotPasswordEmailAuthenticatorCommandHandler(AuthBusinessRules authBusinessRules, IAuthService authenticatorService, IUserService userService, IMapper mapper, IUserRepository userRepository)
        {
            _authBusinessRules = authBusinessRules;
            _authenticatorService = authenticatorService;
            _userService = userService;
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<ForgotPasswordEmailAuthenticatorResponseDto> Handle(ForgotPasswordEmailAuthenticatorCommand request, CancellationToken cancellationToken)
        {
            User? user = await _userService.GetByEmail(request.Email);
            await _authBusinessRules.UserShouldBeExists(user);

            EmailAuthenticator createEmailAuthenticator = await _authenticatorService.CreateEmailAuthenticator(user);

            user.PasswordResetKey = createEmailAuthenticator.ActivationKey;
            User updatedUser = await _userRepository.UpdateAsync(user);

            bool isTheMailSuccess = await _authenticatorService.SendVerificationMail(user.Email, user.FirstName, user.PasswordResetKey);
            await _authBusinessRules.HasTheEmailBeenSentSuccessfully(isTheMailSuccess);

            ForgotPasswordEmailAuthenticatorResponseDto forgotPasswordEmailAuthenticatorResponseDto = _mapper.Map<ForgotPasswordEmailAuthenticatorResponseDto>(updatedUser);
            forgotPasswordEmailAuthenticatorResponseDto.UserId = user.Id;
            return forgotPasswordEmailAuthenticatorResponseDto;
        }
    }
}
