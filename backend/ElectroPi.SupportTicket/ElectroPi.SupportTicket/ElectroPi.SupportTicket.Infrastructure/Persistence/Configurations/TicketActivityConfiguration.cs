using ElectroPi.SupportTicket.Application.Tickets.Activities;
using ElectroPi.SupportTicket.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ElectroPi.SupportTicket.Infrastructure.Persistence.Configurations
{
    public sealed class TicketActivityConfiguration
        : IEntityTypeConfiguration<TicketActivity>
    {
        public void Configure(
            EntityTypeBuilder<TicketActivity> builder)
        {
            builder.ToTable("TicketActivities");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.TicketId)
                .IsRequired();

            builder.Property(x => x.ActorId)
                .IsRequired();

            builder.Property(x => x.ActivityType)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.OccurredAt)
                .IsRequired();

            builder.Property(x => x.Data)
                .HasColumnType("nvarchar(max)");

            builder.HasOne<Ticket>()
                .WithMany()
                .HasForeignKey(x => x.TicketId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}