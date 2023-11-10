using Application.Features.UserOperationClaims.Dtos;
using Application.Features.UserOperationClaims.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Domain.Concrete;
using MediatR;
using static Application.Features.UserOperationClaims.Constants.OperationClaims;
using static Domain.Constants.OperationClaims;

namespace Application.Features.UserOperationClaims.Commands.DeleteUserOperationClaim;

public class DeleteUserOperationClaimCommand : IRequest<DeleteUserOperationClaimDto>//,ISecuredRequest
{
    public int Id { get; set; }

    public string[] Roles => new[] { ADMIN, UserOperationClaimDelete };

    public class DeleteUserOperationClaimCommandHandler : IRequestHandler<DeleteUserOperationClaimCommand, DeleteUserOperationClaimDto>
    {
        private readonly IUserOperationClaimRepository _userOperationClaimDal;
        private readonly IMapper _mapper;
        private readonly UserOperationClaimBusinessRules _userOperationClaimBusinessRules;

        public DeleteUserOperationClaimCommandHandler(IUserOperationClaimRepository userOperationClaimDal, IMapper mapper, UserOperationClaimBusinessRules userOperationClaimBusinessRules)
        {
            _userOperationClaimDal = userOperationClaimDal;
            _mapper = mapper;
            _userOperationClaimBusinessRules = userOperationClaimBusinessRules;
        }

        public async Task<DeleteUserOperationClaimDto> Handle(DeleteUserOperationClaimCommand request, CancellationToken cancellationToken)
        {
            await _userOperationClaimBusinessRules.UserOperationClaimIdMustBeAvailable(request.Id);

            // Mapping the request to a UserOperationClaim object
            UserOperationClaim mappedUserOperationClaim = _mapper.Map<UserOperationClaim>(request);

            // Deleting the mapped user operation claim from the user operation claim data access layer (DAL)
            UserOperationClaim deletedUserOperationClaim = await _userOperationClaimDal.DeleteAsync(mappedUserOperationClaim);

            // Mapping the deleted user operation claim to a DeleteUserOperationClaimDto object
            DeleteUserOperationClaimDto deleteUserOperationClaimDto = _mapper.Map<DeleteUserOperationClaimDto>(deletedUserOperationClaim);

            return deleteUserOperationClaimDto;

        }
    }
}
