
using Hoya.Inventory.Domain.Interfaces;
using MediatR;
using Hoya.Inventory.Domain.Entities;

namespace Hoya.Inventory.Application.BusinessLogic.Invoice
{


    public class InvoiceCreateCommandHandler : IRequestHandler<InvoiceCreateCommand, string>
    {
        private readonly IInvoiceRepository _repo;

        public InvoiceCreateCommandHandler(IInvoiceRepository repo)
        {
            _repo = repo;
        }

        public async Task<string> Handle(InvoiceCreateCommand request, CancellationToken cancellationToken)
        {

            var invoice = new Domain.Entities.Invoice(request.userId);
            var invoiceItems = new List<InvoiceItem>();

            foreach (var item in request.products)
            {

                invoiceItems.Add(new InvoiceItem(
                            item.productId,
                            item.size,
                            item.quantity,
                            item.amount
                ));
            }
            invoice.AddInvoiceItems(invoiceItems);
            invoice.AddDiscount(request.discount);
            invoice.SetNetAmount(request.netAmount);
            invoice.SetPaymentMode(request.paymentMode);
            if(request.exhibitionId != null)
            {
                invoice.SetExhibitionId(request.exhibitionId);
            }

            await _repo.AddAsync(invoice);
            return invoice.Id;
        }
    }
}
