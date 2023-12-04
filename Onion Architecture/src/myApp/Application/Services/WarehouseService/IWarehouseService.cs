using Application.Services.Repositories;
using Core.Persistence.Paging;
using Domain.Concrete;

namespace Application.Services.WarehouseService;

public interface IWarehouseService
{
    Task<List<Warehouse>> GetListWarehouse();
}

public class WarehouseManager : IWarehouseService
{
    private readonly IWarehouseRepository _warehouseRepository;

    public WarehouseManager(IWarehouseRepository warehouseRepository)
    {
        _warehouseRepository = warehouseRepository;
    }

    public async Task<List<Warehouse>> GetListWarehouse()
    {
        IPaginate<Warehouse> warehouses = await _warehouseRepository.GetListAsync();
        return warehouses.Items.ToList();
    }
}