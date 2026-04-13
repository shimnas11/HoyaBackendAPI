using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hoya.Inventory.Application.BusinessLogic.Products;
using Hoya.Inventory.Domain.Entities;
using MediatR;

namespace Hoya.Inventory.Application.BusinessLogic.Exhibitions
{
    public record CreateExhibitionCommand(string name, string place,string runBy,DateTime startDate, DateTime endDate,decimal bookingCost, string userId) : IRequest<string>;
}
