﻿using Core.Application.Dtos;
using Entities.Enums;

namespace Application.Features.Users.Dtos;

public class UpdatedUserResponseDto : IDto
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Address { get; set; }
    public DateTime RegistrationDate { get; set; }
    public bool UserStatus { get; set; }
    public string? ImageUrl { get; set; }
    public string? PasswordResetKey { get; set; }
    public AuthenticatorType AuthenticatorType { get; set; }
}
