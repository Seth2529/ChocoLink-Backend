using ChocoLink.Domain.Entity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChocoLink.Data.EntityFramework.Configuration
{
    public class GroupConfiguration : IEntityTypeConfiguration<Group>
    {
        public void Configure(EntityTypeBuilder<Group> builder)
        {
            builder.ToTable("Group");
            builder.HasKey(g => g.GroupID);

            builder
                .Property(g => g.GroupID)
                .HasColumnName("GroupID")
                .HasColumnType("int");

            builder
                .Property(g => g.GroupName)
                .HasColumnName("GroupName")
                .HasColumnType("varchar(100)")
                .IsRequired();
            builder
                .Property(g => g.NumberParticipants)
                .HasColumnName("NumberParticipants")
                .HasColumnType("int");

            builder
                .Property(g => g.Value)
                .HasColumnName("Value")
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder
                .Property(g => g.DateDiscover)
                .HasColumnName("DateDiscover")
                .HasColumnType("date")
                .IsRequired();

            builder
                .Property(g => g.Description)
                .HasColumnName("Description")
                .HasColumnType("varchar(250)")
                .IsRequired(false);

            builder
                .Property(g => g.Photo)
                .HasColumnName("Photo")
                .HasColumnType("VARBINARY(MAX)");

        }
    }
}