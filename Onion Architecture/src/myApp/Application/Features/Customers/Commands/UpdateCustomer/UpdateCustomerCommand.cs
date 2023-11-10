using Application.Features.Customers.Dtos;
using Application.Features.OperationClaims.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Domain.Concrete;
using MediatR;
using static Application.Features.Customers.Constants.OperationClaims;
using static Domain.Constants.OperationClaims;

namespace Application.Features.Customers.Commands.UpdateCustomer;

public class UpdateCustomerCommand : IRequest<UpdatedCustomerDto>/*, ISecuredRequest*/
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int Demand { get; set; }

    public string[] Roles => new[] { ADMIN, CustomerUpdate };

    public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, UpdatedCustomerDto>
    {
        private readonly IMapper _mapper;
        private readonly CustomerBusinessRules _customerBusinessRules;
        private readonly ICustomerRepository _customerRepository;

        public UpdateCustomerCommandHandler(IMapper mapper, CustomerBusinessRules customerBusinessRules, ICustomerRepository customerRepository)
        {
            _mapper = mapper;
            _customerBusinessRules = customerBusinessRules;
            _customerRepository = customerRepository;
        }

        public async Task<UpdatedCustomerDto> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            await _customerBusinessRules.CustomerIdShouldExistWhenSelected(request.Id);

            Customer mappedCustomer = _mapper.Map<Customer>(request);
            Customer updateCustomer = await _customerRepository.UpdateAsync(mappedCustomer);

            UpdatedCustomerDto updatedCustomerDto = _mapper.Map<UpdatedCustomerDto>(updateCustomer);

            return updatedCustomerDto;
        }
    }
}
