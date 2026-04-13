using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hoya.Inventory.Domain.Entities;

namespace Hoya.Inventory.Domain.Interfaces
{
    public interface IProductRepository
    {
        Task AddAsync(Product product);
        Task<List<Product>> GetAllAsync();
        Task<Product> GetProductByIdAsync(string id);
        Task<bool> IsExist(string name,string code);
        Task UpdateAsync(Product product, string id);
    }
}
