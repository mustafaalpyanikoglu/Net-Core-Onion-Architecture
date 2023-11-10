using Application.Features.Users.Command.CreateUser;
using Application.Features.Users.Command.DeleteUser;
using Application.Features.Users.Command.ResetUserImage;
using Application.Features.Users.Command.UpdateUser;
using Application.Features.Users.Command.UpdateUserFromAuth;
using Application.Features.Users.Dtos;
using Application.Features.Users.Models;
using AutoMapper;
using Core.Persistence.Paging;
using Domain.Concrete;

namespace Application.Features.Users.Profiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User, CreateUserCommand>().ReverseMap();
        CreateMap<User, DeleteUserCommand>().ReverseMap();
        CreateMap<User, UpdateUserCommand>().ReverseMap();
        CreateMap<User, CreatedUserDto>().ReverseMap();
        CreateMap<User, UpdatedUserResponseDto>().ReverseMap();
        CreateMap<User, DeletedUserDto>().ReverseMap();
        CreateMap<User, UpdateUserFromAuthCommand>().ReverseMap();
        CreateMap<User, UpdatedUserFromAuthDto>().ReverseMap();
        CreateMap<User, ResetUserImageRequestDto>().ReverseMap();
        CreateMap<User, ResetUserImageCommand>().ReverseMap();


        CreateMap<User, UserDto>()
             .ForMember(dest => dest.OperationClaimId, opt => opt.MapFrom(src => src.UserOperationClaims.FirstOrDefault().OperationClaimId))
             .ForMember(dest => dest.OperationClaimName, opt => opt.MapFrom(src => src.UserOperationClaims.FirstOrDefault().OperationClaim.Name))
             .ReverseMap();

        CreateMap<IPaginate<User>, UserListModel>().ReverseMap();
        CreateMap<User, UserListDto>()
             .ForMember(dest => dest.OperationClaimId, opt => opt.MapFrom(src => src.UserOperationClaims.FirstOrDefault().OperationClaimId))
             .ForMember(dest => dest.OperationClaimName, opt => opt.MapFrom(src => src.UserOperationClaims.FirstOrDefault().OperationClaim.Name))
             .ReverseMap();


    }
}
