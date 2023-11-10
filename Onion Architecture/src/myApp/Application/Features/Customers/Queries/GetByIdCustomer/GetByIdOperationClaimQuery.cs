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

namespace Application.Features.Customers.Queries.GetByIdCustomer;

public class GetByIdCustomerQuery : IRequest<CustomerDto>//, ISecuredRequest
{
    public int Id { get; set; }

    public string[] Roles => new[] { ADMIN, CustomerGet };

    public class GetByIdCustomerQueryHandler : IRequestHandler<GetByIdCustomerQuery, CustomerDto>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;
        private readonly CustomerBusinessRules _customerBusinessRules;

        public GetByIdCustomerQueryHandler(ICustomerRepository customerRepository, IMapper mapper, CustomerBusinessRules customerBusinessRules)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
            _customerBusinessRules = customerBusinessRules;
        }

        public async Task<CustomerDto> Handle(GetByIdCustomerQuery request, CancellationToken cancellationToken)
        {
            await _customerBusinessRules.CustomerIdShouldExistWhenSelected(request.Id);

            Customer? customer = await _customerRepository.GetAsync(m => m.Id == request.Id);
            CustomerDto customerDto = _mapper.Map<CustomerDto>(customer);

            return customerDto;

        }
    }
}
