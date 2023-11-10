using Application.Features.CustomerWarehouseCosts.Dtos;
using Core.Persistence.Paging;

namespace Application.Features.OperationClaims.Models;

public class CustomerWarehouseCostListModel : BasePageableModel
{
    public IList<CustomerWarehouseCostListDto> Items { get; set; }
}
