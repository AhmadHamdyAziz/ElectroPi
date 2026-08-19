using FluentValidation;


namespace ElectroPi.SupportTicket.Application.Tickets.Commands.CreateTicket
{
    public sealed class CreateTicketValidator
    : AbstractValidator<CreateTicketCommand>
    {
        public CreateTicketValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .MaximumLength(200);

            RuleFor(x => x.Description)
                .NotEmpty();
        }
    }
}
