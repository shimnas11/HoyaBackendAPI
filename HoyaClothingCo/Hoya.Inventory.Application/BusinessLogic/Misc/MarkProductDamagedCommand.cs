using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hoya.Inventory.Application.BusinessLogic.Masters;
using Hoya.Inventory.Domain.Interfaces;
using MediatR;

namespace Hoya.Inventory.Application.BusinessLogic.Misc
{
    public record MarkProductDamagedCommand(
      string ProductId,
      string Size,
      int Quantity,
      string? Reason,
      string? Remarks,
      string adjustmentType
  ) : IRequest<string>;
}
