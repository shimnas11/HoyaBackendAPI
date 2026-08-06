using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hoya.Inventory.Domain.Configurations;
using Hoya.Inventory.Domain.DTO;
using Hoya.Inventory.Domain.Entities;
using Hoya.Inventory.Domain.Entities.Misc;
using Hoya.Inventory.Domain.Interfaces;
using Hoya.Inventory.Infrastructure.Mongo;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Hoya.Inventory.Infrastructure.Repository
{
    public class DamagedProductRepository: IDamagedProductRepository
    {
        private readonly IMongoCollection<DamagedProduct> _damagedProducts;
        private readonly IMongoCollection<Product> _products;

        public DamagedProductRepository(MongoDbContext context, IOptions<MongoDbSettings> settings)
        {
            _damagedProducts = context.Database
                .GetCollection<DamagedProduct>("DamagedProducts");
            _products = context.Database
               .GetCollection<Product>("Products");
        }

        public async Task AddAsync(DamagedProduct product)
        {
            await _damagedProducts.InsertOneAsync(product);
        }

        public async Task<List<DamagedProductDTO>> GetAllAsync()
        {
            var products = await _products
                .Find(x => x.IsActive && !x.IsDeleted)
                .ToListAsync();

            var productLookup = products.ToDictionary(x => x.Id);

            var damages = await _damagedProducts
                .Find(x => x.AdjustmentType == "Damaged")
                .SortByDescending(x => x.CreatedAt)
                .ToListAsync();

            return damages.Select(damage =>
            {
                productLookup.TryGetValue(damage.ProductId, out var product);

                return new DamagedProductDTO
                {
                    Id = damage.Id,
                    ProductId = damage.ProductId,
                    ProductName = product?.Name ?? string.Empty,
                    Color = damage.Color,
                    Size = damage.Size,
                    Quantity = damage.Quantity,
                    BuyingPrice = damage.BuyingPrice,
                    Reason = damage.Reason,
                    Remarks = damage.Remarks,
                    AdjustmentType = damage.AdjustmentType ?? string.Empty,
                    CreatedAt = damage.CreatedAt
                };
            }).ToList();
        }
    }
}
