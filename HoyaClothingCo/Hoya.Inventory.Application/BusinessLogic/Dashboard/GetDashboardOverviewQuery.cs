using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hoya.Inventory.Application.BusinessLogic.Products;
using Hoya.Inventory.Domain.DTO;
using Hoya.Inventory.Domain.Entities;
using MediatR;

namespace Hoya.Inventory.Application.BusinessLogic.Dashboard
{
    public class GetDashboardOverviewQuery : IRequest<DashboardDTO>;
}
