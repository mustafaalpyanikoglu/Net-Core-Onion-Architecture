﻿using Domain.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfiguration;

public partial class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        #region User Model Creation
        builder.ToTable("Users").HasKey(k => k.Id);
        builder.Property(u => u.Id).HasColumnName("Id").UseIdentityColumn(1, 1);
        builder.Property(u => u.FirstName).HasColumnName("FirstName").HasMaxLength(50).IsRequired();
        builder.Property(u => u.LastName).HasColumnName("LastName").HasMaxLength(50).IsRequired();
        builder.Property(u => u.PhoneNumber).HasColumnName("PhoneNumber").HasMaxLength(50).IsRequired();
        builder.Property(u => u.Address).HasColumnName("Address").HasMaxLength(50).IsRequired();
        builder.Property(u => u.Email).HasColumnName("Email").HasMaxLength(200).IsRequired();
        builder.Property(u => u.PasswordHash).HasColumnName("PasswordHash").HasColumnType("varbinary(500)").IsRequired();
        builder.Property(u => u.PasswordSalt).HasColumnName("PasswordSalt").HasColumnType("varbinary(500)").IsRequired();
        builder.Property(u => u.RegistrationDate).HasColumnName("RegistrationDate").IsRequired();
        builder.Property(u => u.UserStatus).HasColumnName("UserStatus").IsRequired();

        builder.HasMany(u => u.UserOperationClaims).WithOne(u => u.User);

        #endregion
    }
}
