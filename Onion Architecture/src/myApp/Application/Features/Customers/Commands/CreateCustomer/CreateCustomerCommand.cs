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

namespace Application.Features.Customers.Commands.CreateCustomer;

public class CreateCustomerCommand:IRequest<CreatedCustomerDto>/*, ISecuredRequest*/
{
    public int UserID { get; set; }
    public int Demand { get; set; }

    public string[] Roles => new[] { ADMIN, CustomerAdd };

    public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, CreatedCustomerDto>
    {
        private readonly IMapper _mapper;
        private readonly CustomerBusinessRules _customerBusinessRules;
        private readonly ICustomerRepository _customerRepository;

        public CreateCustomerCommandHandler(IMapper mapper, CustomerBusinessRules customerBusinessRules, ICustomerRepository customerRepository)
        {
            _mapper = mapper;
            _customerBusinessRules = customerBusinessRules;
            _customerRepository = customerRepository;
        }

        public async Task<CreatedCustomerDto> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            Customer mappedCustomer = _mapper.Map<Customer>(request);
            Customer createdCustomer = await _customerRepository.AddAsync(mappedCustomer);

            CreatedCustomerDto createdCustomerDto = _mapper.Map<CreatedCustomerDto>(createdCustomer);

            return createdCustomerDto;
        }
    }
}
