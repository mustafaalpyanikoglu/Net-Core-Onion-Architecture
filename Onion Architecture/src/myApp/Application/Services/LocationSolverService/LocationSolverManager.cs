using Application.Features.LocationSolvers.Dtos;
using Application.Services.CustomerService;
using Application.Services.WarehouseService;
using AutoMapper;
using Domain.Concrete;

namespace Application.Services.LocationSolverService;

public class LocationSolverManager : ILocationSolverService
{
    private readonly ICustomerService _customerService;
    private readonly IWarehouseService _warehouseService;
    private readonly IMapper _mapper;

    public LocationSolverManager(ICustomerService customerService, IWarehouseService warehouseService, IMapper mapper)
    {
        _customerService = customerService;
        _warehouseService = warehouseService;
        _mapper = mapper;
    }

    public async Task<LocationOptimizationRequestDto> SimaulatedAnnealingQuickSortSolver()
    {
        List<Customer> customers = await _customerService.GetListCustomerWarehouseCosts();
        List<Warehouse> warehouses = await _warehouseService.GetListWarehouse();

        LocationOptimizationRequestDto locationOptimizationRequestDto = new LocationOptimizationRequestDto();
        
        locationOptimizationRequestDto.WarehouseWLPDtos = new WarehouseWLPDtos
        {
            WarehouseWLPDto = _mapper.Map<List<WarehouseWLPDto>>(warehouses)
        };
        
        locationOptimizationRequestDto.CustomerWLPDtos = new CustomerWLPDtos
        {
            CustomerWLPDto = _mapper.Map<List<CustomerWLPDto>>(customers)
        };

        return await Task.FromResult(locationOptimizationRequestDto);
    }
}