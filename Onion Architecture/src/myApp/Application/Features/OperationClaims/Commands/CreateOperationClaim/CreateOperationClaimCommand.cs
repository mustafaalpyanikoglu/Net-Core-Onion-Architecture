using Application.Features.OperationClaims.Dtos;
using Application.Features.OperationClaims.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Domain.Concrete;
using MediatR;
using static Application.Features.OperationClaims.Constants.OperationClaims;
using static Domain.Constants.OperationClaims;

namespace Application.Features.OperationClaims.Commands.CreateOperationClaim;

public class CreateOperationClaimCommand : IRequest<CreatedOperationClaimDto>, ISecuredRequest
{
    public string Name { get; set; }
    public string Description { get; set; }

    public string[] Roles => new[] { ADMIN, OperationClaimAdd };

    public class CreateOperationClaimCommandHandler : IRequestHandler<CreateOperationClaimCommand, CreatedOperationClaimDto>
    {
        private readonly IOperationClaimRepository _operationClaimRepository;
        private readonly IMapper _mapper;
        private readonly OperationClaimBusinessRules _operationClaimBusinessRules;

        public CreateOperationClaimCommandHandler(IOperationClaimRepository operationClaimRepository, IMapper mapper, OperationClaimBusinessRules operationClaimBusinessRules)
        {
            _operationClaimRepository = operationClaimRepository;
            _mapper = mapper;
            _operationClaimBusinessRules = operationClaimBusinessRules;
        }

        public async Task<CreatedOperationClaimDto> Handle(CreateOperationClaimCommand request, CancellationToken cancellationToken)
        {

            await _operationClaimBusinessRules.OperationClaimNameShouldBeNotExists(request.Name);

            // Mapping the request to an OperationClaim object
            OperationClaim mappedOperationClaim = _mapper.Map<OperationClaim>(request);

            // Adding the mapped operation claim to the operation claim repository
            OperationClaim createdOperationClaim = await _operationClaimRepository.AddAsync(mappedOperationClaim);

            // Mapping the created operation claim to a CreatedOperationClaimDto object
            CreatedOperationClaimDto createOperationClaimDto = _mapper.Map<CreatedOperationClaimDto>(createdOperationClaim);

            // Returning the created operation claim DTO
            return createOperationClaimDto;

        }
    }
}
