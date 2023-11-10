using Application.Features.Users.Dtos;
using Application.Features.Users.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Domain.Concrete;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static Application.Features.Users.Constants.OperationClaims;
using static Domain.Constants.OperationClaims;

namespace Application.Features.Users.Queries.GetByIdUser;

public class GetByIdUserQuery : IRequest<UserDto>/*, ISecuredRequest*/
{
    public int Id { get; set; }
    //public string[] Roles => new[] { Admin, UserGet };

    public class GetByIdUserQueryHanlder : IRequestHandler<GetByIdUserQuery, UserDto>
    {
        private readonly IUserRepository _userDal;
        private readonly IMapper _mapper;
        private readonly UserBusinessRules _userBusinessRules;

        public GetByIdUserQueryHanlder(IUserRepository userDal, IMapper mapper, UserBusinessRules userBusinessRules)
        {
            _userDal = userDal;
            _mapper = mapper;
            _userBusinessRules = userBusinessRules;
        }

        public async Task<UserDto> Handle(GetByIdUserQuery request, CancellationToken cancellationToken)
        {
            User? user = await _userDal.GetAsync
                (
                    predicate: u => u.Id == request.Id,
                    include: u => u.Include(u => u.UserOperationClaims).ThenInclude(t => t.OperationClaim)
                );

            await _userBusinessRules.UserShouldBeExist(user);

            UserDto userDto = _mapper.Map<UserDto>(user);

            return userDto;
        }
    }
}
