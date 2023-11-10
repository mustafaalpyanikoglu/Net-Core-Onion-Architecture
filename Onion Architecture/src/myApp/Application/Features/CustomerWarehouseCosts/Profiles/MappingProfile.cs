using Application.Features.CustomerWarehouseCosts.Commands.DeleteCustomerWarehouseCosts;
using Application.Features.CustomerWarehouseCosts.Commands.CreateCustomerWarehouseCost;
using Application.Features.CustomerWarehouseCosts.Commands.UpdateCustomerWarehouseCost;
using Application.Features.CustomerWarehouseCosts.Dtos;
using Application.Features.OperationClaims.Models;
using AutoMapper;
using Core.Persistence.Paging;
using Domain.Concrete;

namespace Application.Features.CustomerWarehouseCosts.Profiles;

public class MappingProfile:Profile
{
    public MappingProfile()
    {
        CreateMap<CustomerWarehouseCost, CreateCustomerWarehouseCostCommand>().ReverseMap();
        CreateMap<CustomerWarehouseCost, DeleteCustomerWarehouseCostCommand>().ReverseMap();
        CreateMap<CustomerWarehouseCost, UpdateCustomerWarehouseCostCommand>().ReverseMap();
        CreateMap<CustomerWarehouseCost, CreatedCustomerWarehouseCostDto>().ReverseMap();
        CreateMap<CustomerWarehouseCost, DeletedCustomerWarehouseCostDto>().ReverseMap();
        CreateMap<CustomerWarehouseCost, UpdatedCustomerWarehouseCostDto>().ReverseMap();
        CreateMap<CustomerWarehouseCost, CustomerWarehouseCostDto>().ReverseMap();
        CreateMap<CustomerWarehouseCost, CustomerWarehouseCostListDto>().ReverseMap();
        CreateMap<IPaginate<CustomerWarehouseCost>, CustomerWarehouseCostListModel>().ReverseMap();
    }
}
