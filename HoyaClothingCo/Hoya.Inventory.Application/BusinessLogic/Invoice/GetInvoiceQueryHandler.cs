
using Hoya.Inventory.Domain.DTO;
using Hoya.Inventory.Domain.Interfaces;
using MediatR;

namespace Hoya.Inventory.Application.BusinessLogic.Invoice
{
    
    public class GetInvoiceQueryHandler : IRequestHandler<GetInvoiceQuery, List<InvoiceDto>>
    {
        private readonly IInvoiceRepository _repo;

        public GetInvoiceQueryHandler(IInvoiceRepository repo)
        {
            _repo = repo;
        }

        public  Task<List<InvoiceDto>> Handle(GetInvoiceQuery request, CancellationToken cancellationToken)
        {
            return _repo.GetAllAsync();
        }
    }
}
