using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hoya.Inventory.Domain.Entities;


using Hoya.Inventory.Domain.DTO;

namespace Hoya.Inventory.Domain.Interfaces
{
    public interface IInvoiceRepository
    {
        Task AddAsync(Invoice invoice);
        Task<List<InvoiceDto>> GetAllAsync();
        Task<Invoice> GetInvoiceByIdAsync(string id);
    }
}
