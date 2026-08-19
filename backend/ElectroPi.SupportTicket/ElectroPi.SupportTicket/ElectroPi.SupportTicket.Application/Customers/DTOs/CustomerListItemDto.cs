
namespace ElectroPi.SupportTicket.Application.Customers.DTOs
{
    public class CustomerListItemDto
    {
        private Guid id;
        private string name;
        private DateTimeOffset createdAt;

        public CustomerListItemDto(Guid id, string name, DateTimeOffset createdAt)
        {
            this.id=id;
            this.name=name;
            this.createdAt=createdAt;
        }
    }
}
