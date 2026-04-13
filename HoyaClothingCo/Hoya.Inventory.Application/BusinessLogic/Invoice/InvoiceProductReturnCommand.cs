using Hoya.Inventory.Domain.DTO;
using Hoya.Inventory.Domain.Entities;
using Hoya.Inventory.Domain.Interfaces;
using MediatR;

namespace Hoya.Inventory.Application.BusinessLogic.Invoice
{
    public record InvoiceProductReturnCommand(string invoiceId, ReturnOrderDTO returnItem
         ) : IRequest<string>;



    public class InvoiceProductReturnCommandHandler : IRequestHandler<InvoiceProductReturnCommand, string>
    {
        private readonly IInvoiceRepository _repo;

        public InvoiceProductReturnCommandHandler(IInvoiceRepository repo)
        {
            _repo = repo;
        }

        public async Task<string> Handle(InvoiceProductReturnCommand request, CancellationToken cancellationToken)
        {
            await _repo.ReturnItemsAsync(request.invoiceId, request.returnItem);
            return "Item return and product updated";

        }
    }
}
