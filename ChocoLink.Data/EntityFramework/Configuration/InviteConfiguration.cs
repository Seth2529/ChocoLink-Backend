using ChocoLink.Domain.Entity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace ChocoLink.Data.EntityFramework.Configuration
{
    public class InviteConfiguration : IEntityTypeConfiguration<Invite>
    {
        public void Configure(EntityTypeBuilder<Invite> builder)
        {
            builder.ToTable("Invite");
            builder.HasKey(i => i.InviteId);

            builder.Property(i => i.InviteId)
                .HasColumnName("InviteId")
                .HasColumnType("int")
                .IsRequired();

            builder.Property(i => i.Email)
                .HasColumnName("Email")
                .HasColumnType("varchar(255)")
                .IsRequired();

            builder.Property(i => i.Status)
                .HasColumnName("Status")
                .HasColumnType("varchar(50)")
                .IsRequired();

            builder.Property(i => i.InvitationDate)
                .HasColumnName("InvitationDate")
                .HasColumnType("datetime")
                .IsRequired();

            builder.Property(i => i.ResponseDate)
                .HasColumnName("ResponseDate")
                .HasColumnType("datetime")
                .IsRequired(false);

            builder.HasOne(i => i.Group)
                .WithMany(i => i.Invites)
                .HasForeignKey(i => i.GroupId);

            builder.HasOne(i => i.User)
                .WithMany(i => i.Invites)
                .HasForeignKey(i => i.UserID);
        }
    }
}

