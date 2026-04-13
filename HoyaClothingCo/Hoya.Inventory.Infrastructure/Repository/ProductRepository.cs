using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hoya.Inventory.Domain.Configurations;
using Hoya.Inventory.Domain.Entities;
using Hoya.Inventory.Domain.Interfaces;
using Hoya.Inventory.Infrastructure.Mongo;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Hoya.Inventory.Infrastructure.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly IMongoCollection<Product> _collection;

        public ProductRepository(MongoDbContext context, IOptions<MongoDbSettings> settings)
        {
            _collection = context.Database
                .GetCollection<Product>("Products");
        }

        public async Task AddAsync(Product product)
        {
            await _collection.InsertOneAsync(product);
        }

        public async Task<List<Product>> GetAllAsync()
        {
            return await _collection.Find(x => x.IsActive && !x.IsDeleted).ToListAsync();
        }

        public async Task<Product> GetProductByIdAsync(string id)
        {
            return await _collection.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<bool> IsExist(string name, string code)
        {
            return await _collection
                 .Find(x => x.Code.ToLower() == code.ToLower())
                    .AnyAsync();
        }
        public async Task UpdateAsync(Product product, string id)
        {
            var filter = Builders<Product>.Filter.Eq(x => x.Id, id);

            await _collection.ReplaceOneAsync(filter, product);
        }

    }
}
