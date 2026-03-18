using Hoya.Inventory.Domain.DTO;
using MediatR;


namespace Hoya.Inventory.Application.BusinessLogic
{
    public record GetInvoiceQuery() : IRequest<List<InvoiceDto>>;

}
