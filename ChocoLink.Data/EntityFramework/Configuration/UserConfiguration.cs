using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ChocoLink.Domain.Entity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChocoLink.Data.EntityFramework.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");
            builder.HasKey(u => u.UserId);

            builder
                .Property(u => u.UserId)
                .HasColumnName("UserId")
                .HasColumnType("int");

            builder
                .Property(u => u.Username)
                .HasColumnName("Username")
                .HasColumnType("varchar(100)");
            builder
                .Property(u => u.Email)
                .HasColumnName("Email")
                .HasColumnType("varchar(200)");

            builder
                .Property(u => u.Password)
                .HasColumnName("Password")
                .HasColumnType("varchar(100)");

            builder
                .Property(u => u.Photo)
                .HasColumnName("Photo")
                .HasColumnType("varbinary(MAX)");

        }
    }
}
