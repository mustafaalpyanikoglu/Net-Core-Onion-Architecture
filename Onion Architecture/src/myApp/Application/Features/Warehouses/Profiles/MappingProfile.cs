using Application.Features.Warehouses.Commands.CreateWarehouse;
using Application.Features.Warehouses.Commands.DeleteWarehouse;
using Application.Features.Warehouses.Commands.UpdateWarehouse;
using Application.Features.Warehouses.Dtos;
using Application.Features.OperationClaims.Models;
using AutoMapper;
using Core.Persistence.Paging;
using Domain.Concrete;

namespace Application.Features.Warehouses.Profiles;

public class MappingProfile:Profile
{
    public MappingProfile()
    {
        CreateMap<Warehouse, CreateWarehouseCommand>().ReverseMap();
        CreateMap<Warehouse, DeleteWarehouseCommand>().ReverseMap();
        CreateMap<Warehouse, UpdateWarehouseCommand>().ReverseMap();
        CreateMap<Warehouse, CreatedWarehouseDto>().ReverseMap();
        CreateMap<Warehouse, DeletedWarehouseDto>().ReverseMap();
        CreateMap<Warehouse, UpdatedWarehouseDto>().ReverseMap();
        CreateMap<Warehouse, WarehouseDto>().ReverseMap();
        CreateMap<Warehouse, WarehouseListDto>().ReverseMap();
        CreateMap<IPaginate<Warehouse>, WarehouseListModel>().ReverseMap();
    }
}
