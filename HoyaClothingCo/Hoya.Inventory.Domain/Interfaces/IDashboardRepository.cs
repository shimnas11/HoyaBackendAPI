using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hoya.Inventory.Domain.DTO;

namespace Hoya.Inventory.Domain.Interfaces
{
    public interface IDashboardRepository
    {
        Task<DashboardDTO> GetDashboardOverviewAsync();
    }
}
