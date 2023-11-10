using Application.Features.Warehouses.Dtos;
using Core.Persistence.Paging;

namespace Application.Features.OperationClaims.Models;

public class WarehouseListModel : BasePageableModel
{
    public IList<WarehouseListDto> Items { get; set; }
}
