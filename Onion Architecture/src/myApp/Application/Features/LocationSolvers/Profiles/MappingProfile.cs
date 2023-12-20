using Application.Features.LocationSolvers.Dtos;
using AutoMapper;
using Domain.Concrete;

namespace Application.Features.LocationSolvers.Profiles;

public class MappingProfile:Profile
{
    public MappingProfile()
    {
        CreateMap<Warehouse, WarehouseWLPDto>().ReverseMap();

        CreateMap<Customer, CustomerWLPDto>()
            .ForMember(dest => dest.CustomerWarehouseCostWlpDtos, opt => opt.MapFrom(src => src.CustomerWarehouseCosts))
            .ReverseMap();

        CreateMap<CustomerWarehouseCost, CustomerWarehouseCostWlpDto>().ReverseMap();

        CreateMap<HashSet<CustomerWarehouseCost>, ListCustomerWarehouseCosts>()
            .ForMember(dest => dest.CustomerWarehouseCostWlpDto, opt => opt.MapFrom(src => src));
    }
}
