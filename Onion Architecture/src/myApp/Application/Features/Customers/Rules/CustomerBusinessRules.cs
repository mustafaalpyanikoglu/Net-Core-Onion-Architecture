using Application.Features.OperationClaims.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Concrete;
using System.Security.Policy;

namespace Application.Features.OperationClaims.Rules;

public class CustomerBusinessRules : BaseBusinessRules
{
    private readonly ICustomerRepository _customerRepository;

    public CustomerBusinessRules(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task CustomerIdShouldExistWhenSelected(int? id)
    {
        Customer? result = await _customerRepository.GetAsync(b => b.Id == id);
        if (result == null) throw new BusinessException(OperationClaimMessages.OperationClaimNotFound);
    }

}
