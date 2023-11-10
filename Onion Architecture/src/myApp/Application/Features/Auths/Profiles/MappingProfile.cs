using Application.Features.Auths.Commands.ChangePassword;
using Application.Features.Auths.Commands.EnableEmailAuthenticator;
using Application.Features.Auths.Commands.ForgotPasswordEmailAuthenticator;
using Application.Features.Auths.Dtos;
using AutoMapper;
using Domain.Concrete;

namespace Application.Features.Auths.Profiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User, ChangePasswordCommand>().ReverseMap();
        CreateMap<User, UserForChangePasswordDto>().ReverseMap();
        CreateMap<RefreshToken, RevokedTokenDto>().ReverseMap();
        CreateMap<EnableEmailAuthenticatorCommand, EnableEmailAuthenticatorResponseDto>().ReverseMap();
        CreateMap<EmailAuthenticator, EnableEmailAuthenticatorResponseDto>().ReverseMap();
        CreateMap<ForgotPasswordEmailAuthenticatorCommand, ForgotPasswordEmailAuthenticatorResponseDto>().ReverseMap();
        CreateMap<EmailAuthenticator, ForgotPasswordEmailAuthenticatorResponseDto>().ReverseMap();
        CreateMap<User, ForgotPasswordEmailAuthenticatorResponseDto>().ReverseMap();
    }
}
