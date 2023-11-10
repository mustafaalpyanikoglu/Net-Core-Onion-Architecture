using Application.Features.Customers.Dtos;
using Core.Persistence.Paging;

namespace Application.Features.OperationClaims.Models;

public class CustomerListModel : BasePageableModel
{
    public IList<CustomerListDto> Items { get; set; }
}
