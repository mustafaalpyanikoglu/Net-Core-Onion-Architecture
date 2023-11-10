using Core.Application.Dtos;

namespace Application.Features.Customers.Dtos;

public class CreatedCustomerDto:IDto
{
    public int UserID { get; set; }
    public int Demand { get; set; }
}
