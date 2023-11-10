using Application.Features.Warehouses.Dtos;
using Application.Features.OperationClaims.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Domain.Concrete;
using MediatR;
using static Application.Features.Warehouses.Constants.OperationClaims;
using static Domain.Constants.OperationClaims;

namespace Application.Features.Warehouses.Commands.UpdateWarehouse;

public class UpdateWarehouseCommand : IRequest<UpdatedWarehouseDto>/*, ISecuredRequest*/
{
    public int Id { get; set; }
    public int Capacity { get; set; }
    public double SetupCost { get; set; }

    public string[] Roles => new[] { ADMIN, WarehouseUpdate };

    public class UpdateWarehouseCommandHandler : IRequestHandler<UpdateWarehouseCommand, UpdatedWarehouseDto>
    {
        private readonly IMapper _mapper;
        private readonly WarehouseBusinessRules _warehouseBusinessRules;
        private readonly IWarehouseRepository _warehouseRepository;

        public UpdateWarehouseCommandHandler(IMapper mapper, WarehouseBusinessRules warehouseBusinessRules, IWarehouseRepository warehouseRepository)
        {
            _mapper = mapper;
            _warehouseBusinessRules = warehouseBusinessRules;
            _warehouseRepository = warehouseRepository;
        }

        public async Task<UpdatedWarehouseDto> Handle(UpdateWarehouseCommand request, CancellationToken cancellationToken)
        {
            await _warehouseBusinessRules.WarehouseIdShouldExistWhenSelected(request.Id);

            Warehouse mappedWarehouse = _mapper.Map<Warehouse>(request);
            Warehouse updateWarehouse = await _warehouseRepository.UpdateAsync(mappedWarehouse);
            UpdatedWarehouseDto updatedWarehouseDto = _mapper.Map<UpdatedWarehouseDto>(updateWarehouse);

            return updatedWarehouseDto;
        }
    }
}
