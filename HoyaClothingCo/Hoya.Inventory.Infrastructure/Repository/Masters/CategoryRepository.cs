using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hoya.Inventory.Domain.Configurations;
using Hoya.Inventory.Domain.Entities;
using Hoya.Inventory.Domain.Entities.Masters;
using Hoya.Inventory.Domain.Interfaces.Masters;
using Hoya.Inventory.Infrastructure.Mongo;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Hoya.Inventory.Infrastructure.Repository.Masters
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly IMongoCollection<Category> _categories;
        public CategoryRepository(MongoDbContext context, IOptions<MongoDbSettings> settings)
        {
            _categories = context.Database
                .GetCollection<Category>("Category");
        }
        public async Task AddAsync(Category catagory)
        {
          await  _categories.InsertOneAsync(catagory);
        }

        public Task<List<Category>> GetAllAsync()
        {
            throw new NotImplementedException();
        }
    }

}
