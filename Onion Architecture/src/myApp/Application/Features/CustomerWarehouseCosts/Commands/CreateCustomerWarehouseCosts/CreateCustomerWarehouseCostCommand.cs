using Application.Features.CustomerWarehouseCosts.Dtos;
using Application.Features.OperationClaims.Dtos;
using Application.Features.OperationClaims.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Domain.Concrete;
using MediatR;
using static Application.Features.CustomerWarehouseCosts.Constants.OperationClaims;
using static Domain.Constants.OperationClaims;

namespace Application.Features.CustomerWarehouseCosts.Commands.CreateCustomerWarehouseCost;

public class CreateCustomerWarehouseCostCommand:IRequest<CreatedCustomerWarehouseCostDto>/*, ISecuredRequest*/
{
    public int Capacity { get; set; }
    public double SetupCost { get; set; }

    public string[] Roles => new[] { ADMIN, CustomerWarehouseCostAdd };

    public class CreateCustomerWarehouseCostCommandHandler : IRequestHandler<CreateCustomerWarehouseCostCommand, CreatedCustomerWarehouseCostDto>
    {
        private readonly ICustomerWarehouseCostRepository _customerWarehouseCostRepository;
        private readonly IMapper _mapper;
        private readonly CustomerWarehouseCostBusinessRules _customerWarehouseCostBusinessRules;

        public CreateCustomerWarehouseCostCommandHandler(ICustomerWarehouseCostRepository customerWarehouseCostRepository, IMapper mapper, CustomerWarehouseCostBusinessRules customerWarehouseCostBusinessRules)
        {
            _customerWarehouseCostRepository = customerWarehouseCostRepository;
            _mapper = mapper;
            _customerWarehouseCostBusinessRules = customerWarehouseCostBusinessRules;
        }

        public async Task<CreatedCustomerWarehouseCostDto> Handle(CreateCustomerWarehouseCostCommand request, CancellationToken cancellationToken)
        {
            CustomerWarehouseCost mappedCustomerWarehouseCost = _mapper.Map<CustomerWarehouseCost>(request);
            CustomerWarehouseCost createdCustomerWarehouseCost = await _customerWarehouseCostRepository.AddAsync(mappedCustomerWarehouseCost);

            CreatedCustomerWarehouseCostDto createdCustomerWarehouseCostDto = _mapper.Map<CreatedCustomerWarehouseCostDto>(createdCustomerWarehouseCost);

            return createdCustomerWarehouseCostDto;
        }
    }
}
