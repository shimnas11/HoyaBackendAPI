using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hoya.Inventory.Domain.Entities;

namespace Hoya.Inventory.Domain.Interfaces
{
    public interface IUserRepository
    {
       Task AddAsync(string email, string password);
       Task<User> GetAsync(string email);
    }
}
