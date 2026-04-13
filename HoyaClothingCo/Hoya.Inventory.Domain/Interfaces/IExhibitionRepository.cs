using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hoya.Inventory.Domain.DTO;
using Hoya.Inventory.Domain.Entities;

namespace Hoya.Inventory.Domain.Interfaces
{
    public interface IExhibitionRepository
    {
        Task AddAsync(Entities.Exhibition.Exhibition exhibition);
        Task UpdateAsync(Entities.Exhibition.Exhibition exhibition);
        Task<Entities.Exhibition.Exhibition> GetByIdAsync(string id);
        Task<ExhibitionDTO> GetDetails(string id);
        Task<ExhibitionOverviewDTO> GetOverview(string id);
        Task<List<Entities.Exhibition.Exhibition>> GetAll();
    }
}
