using Application.Features.OperationClaims.Models;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Domain.Concrete;
using MediatR;
using static Application.Features.Warehouses.Constants.OperationClaims;
using static Domain.Constants.OperationClaims;

namespace Application.Features.Warehouses.Queries.GetListWarehouse;

public class GetListWarehouseQuery : IRequest<WarehouseListModel>//, ISecuredRequest
{
    public PageRequest PageRequest { get; set; }
    public string[] Roles => new[] { ADMIN, WarehouseGet };

    public class GetListWarehouseQueryHanlder : IRequestHandler<GetListWarehouseQuery, WarehouseListModel>
    {
        private readonly IWarehouseRepository _warehouseRepository;
        private readonly IMapper _mapper;

        public GetListWarehouseQueryHanlder(IWarehouseRepository warehouseRepository, IMapper mapper)
        {
            _warehouseRepository = warehouseRepository;
            _mapper = mapper;
        }

        public async Task<WarehouseListModel> Handle(GetListWarehouseQuery request, CancellationToken cancellationToken)
        {
            IPaginate<Warehouse> warehouses = await _warehouseRepository.GetListAsync(
                index: request.PageRequest.Page,
                size: request.PageRequest.PageSize);

            WarehouseListModel mappedWarehouseListModel = _mapper.Map<WarehouseListModel>(warehouses);
            return mappedWarehouseListModel;
        }
    }
}
