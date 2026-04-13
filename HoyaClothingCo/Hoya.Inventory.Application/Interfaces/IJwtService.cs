using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hoya.Inventory.Domain.Entities;

namespace Hoya.Inventory.Application.Interfaces
{
    public interface IJwtService
    {
        string GenerateToken(string userId, string email);
        Task<User> GetUser(string email);

        Task AddUserAsync(string email,string password);
    }
}
