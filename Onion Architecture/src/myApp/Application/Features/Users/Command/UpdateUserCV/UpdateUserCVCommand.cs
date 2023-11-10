using Application.Services.Repositories;
using AutoMapper;
using Application.Services.UserService;
using Domain.Concrete;
using MediatR;
using Microsoft.AspNetCore.Http;
using static Application.Features.Users.Constants.OperationClaims;
using static Domain.Constants.OperationClaims;
using Application.Features.Users.Dtos;
using Application.Features.Users.Rules;

namespace Application.Features.Users.Command.UpdateUserCV;

public class UpdateUserCVCommand : IRequest<UpdatedUserResponseDto>/*,ISecuredRequest*/
{
    #region Request Requirements
    public int Id { get; set; }
    public IFormFile? File { get; set; }
    #endregion

    //public string[] Roles => new[] { Admin, UserUpdate };

    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCVCommand, UpdatedUserResponseDto>
    {
        private readonly IUserRepository _userDal;
        private readonly IMapper _mapper;
        private readonly UserBusinessRules _userBusinessRules;
        private readonly IUserService _userService;

        public UpdateUserCommandHandler(IUserRepository userDal, IMapper mapper, UserBusinessRules userBusinessRules, IUserService userService)
        {
            _userDal = userDal;
            _mapper = mapper;
            _userBusinessRules = userBusinessRules;
            _userService = userService;
        }

        public async Task<UpdatedUserResponseDto> Handle(UpdateUserCVCommand request, CancellationToken cancellationToken)
        {
            User? user = await _userDal.GetAsync(u => u.Id == request.Id);
            await _userBusinessRules.UserShouldBeExist(user);

            User updatedUser = await _userDal.UpdateAsync(user);
            UpdatedUserResponseDto updatedUserDto = _mapper.Map<UpdatedUserResponseDto>(updatedUser);

            return updatedUserDto;
        }
    }
}
