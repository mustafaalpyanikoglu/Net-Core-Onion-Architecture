using AutoMapper;
using MediatR;
using static Application.Features.OperationClaims.Constants.OperationClaims;
using static Domain.Constants.OperationClaims;
using Core.Application.Pipelines.Authorization;
using Application.Services.Repositories;
using Domain.Concrete;
using Application.Features.OperationClaims.Dtos;
using Application.Features.OperationClaims.Rules;

namespace Application.Features.OperationClaims.Commands.DeleteOperationClaim;

public class DeleteOperationClaimCommand : IRequest<DeletedOperationClaimDto>, ISecuredRequest
{
    public int Id { get; set; }

    public string[] Roles => new[] { ADMIN, OperationClaimDelete };

    public class DeleteOperationClaimCommandHandler : IRequestHandler<DeleteOperationClaimCommand, DeletedOperationClaimDto>
    {
        private readonly IOperationClaimRepository _operationClaimRepository;
        private readonly IMapper _mapper;
        private readonly OperationClaimBusinessRules _operationClaimBusinessRules;

        public DeleteOperationClaimCommandHandler(IOperationClaimRepository operationClaimRepository, IMapper mapper, OperationClaimBusinessRules operationClaimBusinessRules)
        {
            _operationClaimRepository = operationClaimRepository;
            _mapper = mapper;
            _operationClaimBusinessRules = operationClaimBusinessRules;
        }

        public async Task<DeletedOperationClaimDto> Handle(DeleteOperationClaimCommand request, CancellationToken cancellationToken)
        {
            await _operationClaimBusinessRules.OperationClaimIdShouldExistWhenSelected(request.Id);

            // Mapping the request to an OperationClaim object
            OperationClaim mappedOperationClaim = _mapper.Map<OperationClaim>(request);

            // Deleting the mapped operation claim from the operation claim repository
            OperationClaim deletedOperationClaim = await _operationClaimRepository.DeleteAsync(mappedOperationClaim);

            // Mapping the deleted operation claim to a DeletedOperationClaimDto object
            DeletedOperationClaimDto deleteOperationClaimDto = _mapper.Map<DeletedOperationClaimDto>(deletedOperationClaim);

            return deleteOperationClaimDto;
        }
    }
}
