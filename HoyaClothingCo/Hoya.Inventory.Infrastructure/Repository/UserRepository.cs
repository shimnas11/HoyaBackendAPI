using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hoya.Inventory.Domain.Configurations;
using Hoya.Inventory.Domain.Entities;
using Hoya.Inventory.Domain.Interfaces;
using Hoya.Inventory.Infrastructure.Mongo;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Hoya.Inventory.Infrastructure.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoCollection<User> _users;
        private readonly MongoDbContext _context;
        public UserRepository(MongoDbContext context, IOptions<MongoDbSettings> settings)
        {
            _context = context;
            _users = context.Database
                .GetCollection<User>("Users");
        }

        public async Task AddAsync(string email, string password)
        {
            var user = new User
            {
                Email = email,
                Password = password,
                Id = Guid.NewGuid().ToString()
            };

            await _users.InsertOneAsync(user);
        }

        public async Task<User> GetAsync(string email)
        {
            return  await _users
             .Find(x => x.Email == email)
             .FirstOrDefaultAsync();
        }
    }
}
