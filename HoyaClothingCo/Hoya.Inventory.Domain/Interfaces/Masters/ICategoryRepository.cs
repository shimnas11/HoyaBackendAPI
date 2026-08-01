using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hoya.Inventory.Domain.DTO;
using Hoya.Inventory.Domain.Entities;
using Hoya.Inventory.Domain.Entities.Masters;

namespace Hoya.Inventory.Domain.Interfaces.Masters
{
    public interface ICategoryRepository
    {
        Task AddAsync(Category catagory);
        Task<List<Category>> GetAllAsync();
    }
}
