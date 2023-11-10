using Application.Features.OperationClaims.Models;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Domain.Concrete;
using MediatR;
using static Application.Features.CustomerWarehouseCosts.Constants.OperationClaims;
using static Domain.Constants.OperationClaims;

namespace Application.Features.CustomerWarehouseCosts.Queries.GetListCustomerWarehouseCost;

public class GetListCustomerWarehouseCostQuery : IRequest<CustomerWarehouseCostListModel>//, ISecuredRequest
{
    public PageRequest PageRequest { get; set; }
    public string[] Roles => new[] { ADMIN, CustomerWarehouseCostGet };

    public class GetListCustomerWarehouseCostQueryHanlder : IRequestHandler<GetListCustomerWarehouseCostQuery, CustomerWarehouseCostListModel>
    {
        private readonly ICustomerWarehouseCostRepository _customerWarehouseCostRepository;
        private readonly IMapper _mapper;

        public GetListCustomerWarehouseCostQueryHanlder(ICustomerWarehouseCostRepository customerWarehouseCostRepository, IMapper mapper)
        {
            _customerWarehouseCostRepository = customerWarehouseCostRepository;
            _mapper = mapper;
        }

        public async Task<CustomerWarehouseCostListModel> Handle(GetListCustomerWarehouseCostQuery request, CancellationToken cancellationToken)
        {
            IPaginate<CustomerWarehouseCost> warehouses = await _customerWarehouseCostRepository.GetListAsync(
                index: request.PageRequest.Page,
                size: request.PageRequest.PageSize);

            CustomerWarehouseCostListModel mappedCustomerWarehouseCostListModel = _mapper.Map<CustomerWarehouseCostListModel>(warehouses);
            return mappedCustomerWarehouseCostListModel;
        }
    }
}
