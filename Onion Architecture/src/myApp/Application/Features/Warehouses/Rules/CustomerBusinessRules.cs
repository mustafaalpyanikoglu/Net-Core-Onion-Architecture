using Application.Features.OperationClaims.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Concrete;
using System.Security.Policy;

namespace Application.Features.OperationClaims.Rules;

public class WarehouseBusinessRules : BaseBusinessRules
{
    private readonly IWarehouseRepository _warehouseRepository;

    public WarehouseBusinessRules(IWarehouseRepository warehouseRepository)
    {
        _warehouseRepository = warehouseRepository;
    }

    public async Task WarehouseIdShouldExistWhenSelected(int? id)
    {
        Warehouse? result = await _warehouseRepository.GetAsync(b => b.Id == id);
        if (result == null) throw new BusinessException(OperationClaimMessages.OperationClaimNotFound);
    }

}
