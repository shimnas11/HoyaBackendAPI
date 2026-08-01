using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hoya.Inventory.Application.BusinessLogic.Invoice;
using MediatR;

namespace Hoya.Inventory.Application.BusinessLogic.Masters
{

    public record CreateCategoryCommand(string name
    ) : IRequest<string>;

}
