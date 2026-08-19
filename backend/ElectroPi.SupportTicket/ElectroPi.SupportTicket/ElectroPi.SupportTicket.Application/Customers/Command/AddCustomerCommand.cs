using ElectroPi.SupportTicket.Application.Abstractions.Identity;
using ElectroPi.SupportTicket.Application.Abstractions.Persistence;
using ElectroPi.SupportTicket.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectroPi.SupportTicket.Application.Customers.Command
{
    public sealed record AddCustomerCommand(
            string Name
        )
        : IRequest;

    public sealed class AddCustomerCommandHandler(
        IAppDbContext db,
        ICurrentUser currentUser)
        : IRequestHandler<AddCustomerCommand>
    {
        public async Task Handle(AddCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = Customer.Create(
                name: request.Name,
                currentUser.UserId.Value
            );

            if (db.Customers.Any(c=>c.Name.Equals(request.Name)))
            {
                throw new InvalidOperationException("Customer already exists.");
            }

            await db.Customers.AddAsync(customer, cancellationToken);

            await db.SaveChangesAsync(cancellationToken);
        }
    }
}
