using Application.Features.Warehouses.Dtos;
using Application.Features.OperationClaims.Dtos;
using Application.Features.OperationClaims.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Domain.Concrete;
using MediatR;
using static Application.Features.Warehouses.Constants.OperationClaims;
using static Domain.Constants.OperationClaims;

namespace Application.Features.Warehouses.Commands.CreateWarehouse;

public class CreateWarehouseCommand:IRequest<CreatedWarehouseDto>/*, ISecuredRequest*/
{
    public int Capacity { get; set; }
    public double SetupCost { get; set; }

    public string[] Roles => new[] { ADMIN, WarehouseAdd };

    public class CreateWarehouseCommandHandler : IRequestHandler<CreateWarehouseCommand, CreatedWarehouseDto>
    {
        private readonly IMapper _mapper;
        private readonly WarehouseBusinessRules _warehouseBusinessRules;
        private readonly IWarehouseRepository _warehouseRepository;

        public CreateWarehouseCommandHandler(IMapper mapper, WarehouseBusinessRules warehouseBusinessRules, IWarehouseRepository warehouseRepository)
        {
            _mapper = mapper;
            _warehouseBusinessRules = warehouseBusinessRules;
            _warehouseRepository = warehouseRepository;
        }

        public async Task<CreatedWarehouseDto> Handle(CreateWarehouseCommand request, CancellationToken cancellationToken)
        {
            Warehouse mappedWarehouse = _mapper.Map<Warehouse>(request);
            Warehouse createdWarehouse = await _warehouseRepository.AddAsync(mappedWarehouse);

            CreatedWarehouseDto createdWarehouseDto = _mapper.Map<CreatedWarehouseDto>(createdWarehouse);

            return createdWarehouseDto;
        }
    }
}
