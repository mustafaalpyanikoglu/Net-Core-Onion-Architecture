using Application.Features.CustomerWarehouseCosts.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Concrete;

namespace Application.Features.OperationClaims.Rules;

public class CustomerWarehouseCostBusinessRules : BaseBusinessRules
{
    private readonly ICustomerWarehouseCostRepository _customerWarehouseCostRepository;

    public CustomerWarehouseCostBusinessRules(ICustomerWarehouseCostRepository customerWarehouseCostRepository)
    {
        _customerWarehouseCostRepository = customerWarehouseCostRepository;
    }

    public async Task CustomerWarehouseCostIdShouldExistWhenSelected(int? id)
    {
        CustomerWarehouseCost? result = await _customerWarehouseCostRepository.GetAsync(b => b.Id == id);
        if (result == null) throw new BusinessException(CustomerWarehouseCostMessages.CustomerWarehouseCostNotFound);
    }

}
