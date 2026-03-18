using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hoya.Inventory.Application.BusinessLogic.Products;
using Hoya.Inventory.Application.DTOs;
using MediatR;

namespace Hoya.Inventory.Application.BusinessLogic.Invoice
{

    public record InvoiceProductCommand(string productId,

        string size,
        int quantity,
         decimal amount
        ) : IRequest;
    public record InvoiceCreateCommand(string userId,List<InvoiceProductCommand> products,decimal discount,decimal netAmount,string paymentMode,string exhibitionId) : IRequest<string>;
}
