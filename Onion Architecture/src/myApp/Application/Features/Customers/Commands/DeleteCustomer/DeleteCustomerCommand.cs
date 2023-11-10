using Application.Features.Customers.Dtos;
using Application.Features.OperationClaims.Dtos;
using Application.Features.OperationClaims.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Domain.Concrete;
using MediatR;
using static Application.Features.Customers.Constants.OperationClaims;
using static Domain.Constants.OperationClaims;

namespace Application.Features.Customers.Commands.DeleteCustomer;

public class DeleteCustomerCommand : IRequest<DeletedCustomerDto>/*, ISecuredRequest*/
{
    public int Id { get; set; }

    public string[] Roles => new[] { ADMIN, CustomerDelete };

    public class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand, DeletedCustomerDto>
    {
        private readonly IMapper _mapper;
        private readonly CustomerBusinessRules _customerBusinessRules;
        private readonly ICustomerRepository _customerRepository;

        public DeleteCustomerCommandHandler(IMapper mapper, CustomerBusinessRules customerBusinessRules, ICustomerRepository customerRepository)
        {
            _mapper = mapper;
            _customerBusinessRules = customerBusinessRules;
            _customerRepository = customerRepository;
        }

        public async Task<DeletedCustomerDto> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
        {
            await _customerBusinessRules.CustomerIdShouldExistWhenSelected(request.Id);

            Customer mappedCustomer = _mapper.Map<Customer>(request);
            Customer deletedCustomer = await _customerRepository.DeleteAsync(mappedCustomer);

            DeletedCustomerDto deletedCustomerDto = _mapper.Map<DeletedCustomerDto>(deletedCustomer);

            return deletedCustomerDto;
        }
    }
}
