using ElectroPi.SupportTicket.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ElectroPi.SupportTicket.Infrastructure.Persistence.Configurations
{
    public sealed class TicketConfiguration
    : IEntityTypeConfiguration<Ticket>
    {
        public void Configure(
            EntityTypeBuilder<Ticket> builder)
        {
            builder.ToTable("Tickets");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Title)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(x => x.Description)
                .IsRequired();

            builder.Property(x => x.Status)
                .IsRequired();

            builder.Property(x => x.Priority)
                .IsRequired();

            builder.Property(x => x.CustomerId)
                .IsRequired();

            builder.Property(x => x.AssignedAgentId);

            builder.HasMany(x => x.Comments)
                .WithOne()
                .HasForeignKey(x => x.TicketId);

            builder.HasMany(x => x.TimeEntries)
                .WithOne()
                .HasForeignKey(x => x.TicketId);

            builder.HasOne<User>()
                .WithMany()
                .HasForeignKey(x => x.AssignedAgentId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}