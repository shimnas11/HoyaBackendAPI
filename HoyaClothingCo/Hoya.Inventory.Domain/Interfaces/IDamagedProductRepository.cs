using Hoya.Inventory.Domain.DTO;
using Hoya.Inventory.Domain.Entities.Misc;

namespace Hoya.Inventory.Domain.Interfaces
{
    public interface IDamagedProductRepository
    {
        Task AddAsync(DamagedProduct product);
        Task<List<DamagedProductDTO>> GetAllAsync();
    }
}
