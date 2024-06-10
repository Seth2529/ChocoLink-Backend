using ChocoLink.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChocoLink.Data.EntityFramework.Configuration
{
    public class GroupUserConfiguration : IEntityTypeConfiguration<GroupUser>
    {
        public void Configure(EntityTypeBuilder<GroupUser> builder)
        {
            builder.ToTable("GroupUsers");
            builder.HasKey(gu => gu.GroupUserID);

            builder.Property(gu => gu.GroupUserID)
                .HasColumnName("GroupUserID")
                .HasColumnType("int")
                .IsRequired();

            builder.Property(gu => gu.GroupID)
                .HasColumnName("GroupID")
                .HasColumnType("int")
                .IsRequired();

            builder.Property(gu => gu.UserID)
                .HasColumnName("UserID")
                .HasColumnType("int")
                .IsRequired();

            builder.HasOne(gu => gu.Group)
                .WithMany(g => g.GroupUsers)
                .HasForeignKey(gu => gu.GroupID);

            builder.HasOne(gu => gu.User)
                .WithMany(u => u.GroupUsers)
                .HasForeignKey(gu => gu.UserID);
        }
    }
}
