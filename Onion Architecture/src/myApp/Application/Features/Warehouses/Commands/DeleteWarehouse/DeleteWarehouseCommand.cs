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

namespace Application.Features.Warehouses.Commands.DeleteWarehouse;

public class DeleteWarehouseCommand : IRequest<DeletedWarehouseDto>, ISecuredRequest
{
    public int Id { get; set; }

    public string[] Roles => new[] { ADMIN, WarehouseDelete };

    public class DeleteWarehouseCommandHandler : IRequestHandler<DeleteWarehouseCommand, DeletedWarehouseDto>
    {
        private readonly IMapper _mapper;
        private readonly WarehouseBusinessRules _warehouseBusinessRules;
        private readonly IWarehouseRepository _warehouseRepository;

        public DeleteWarehouseCommandHandler(IMapper mapper, WarehouseBusinessRules warehouseBusinessRules, IWarehouseRepository warehouseRepository)
        {
            _mapper = mapper;
            _warehouseBusinessRules = warehouseBusinessRules;
            _warehouseRepository = warehouseRepository;
        }

        public async Task<DeletedWarehouseDto> Handle(DeleteWarehouseCommand request, CancellationToken cancellationToken)
        {
            await _warehouseBusinessRules.WarehouseIdShouldExistWhenSelected(request.Id);

            Warehouse mappedWarehouse = _mapper.Map<Warehouse>(request);
            Warehouse deletedWarehouse = await _warehouseRepository.DeleteAsync(mappedWarehouse);

            DeletedWarehouseDto deletedWarehouseDto = _mapper.Map<DeletedWarehouseDto>(deletedWarehouse);

            return deletedWarehouseDto;
        }
    }
}
