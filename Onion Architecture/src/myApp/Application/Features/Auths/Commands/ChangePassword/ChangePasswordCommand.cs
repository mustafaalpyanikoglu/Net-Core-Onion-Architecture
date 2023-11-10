using Application.Features.Auths.Dtos;
using Application.Features.Auths.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Security.Hashing;
using Domain.Concrete;
using MediatR;
using static Domain.Constants.OperationClaims;

namespace Application.Features.Auths.Commands.ChangePassword;

public class ChangePasswordCommand : IRequest<UserForChangePasswordDto>
{
    public string Email { get; set; }
    public string PasswordResetKey { get; set; }
    public string NewPassword { get; set; }
    public string RepeatPassword { get; set; }


    public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, UserForChangePasswordDto>
    {
        private readonly IMapper _mapper;
        private readonly AuthBusinessRules _authBusinessRules;
        private readonly IUserRepository _userRepository;

        public ChangePasswordCommandHandler(IMapper mapper, AuthBusinessRules authBusinessRules, IUserRepository userRepository)
        {
            _mapper = mapper;
            _authBusinessRules = authBusinessRules;
            _userRepository = userRepository;
        }

        public async Task<UserForChangePasswordDto> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            User? user = await _userRepository.GetAsync(u => u.Email == request.Email);
            await _authBusinessRules.UserShouldBeExists(user);

            await _authBusinessRules.ActivationCodeMustMatchTheOneInTheDatabase(user.Id, request.PasswordResetKey);
            await _authBusinessRules.PasswordsEnteredMustBeTheSame(request.NewPassword, request.RepeatPassword);

            byte[] passwordHash, passwordSalt;

            HashingHelper.CreatePasswordHash(request.NewPassword, out passwordHash, out passwordSalt);

            user.PasswordSalt = passwordSalt;
            user.PasswordHash = passwordHash;
            user.PasswordResetKey = null;
            User updatedUser = await _userRepository.UpdateAsync(user);

            UserForChangePasswordDto updatedUserDto = _mapper.Map<UserForChangePasswordDto>(updatedUser);

            updatedUserDto.Password = request.NewPassword;

            return updatedUserDto;
        }
    }
}
