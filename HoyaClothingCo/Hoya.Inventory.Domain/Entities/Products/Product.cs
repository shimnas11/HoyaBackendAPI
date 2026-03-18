using Hoya.Inventory.Domain.Enums;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace Hoya.Inventory.Domain.Entities
{
    public class Product : AggregateRoot
    {


        public string Name { get; private set; }
        public string Code { get; private set; }
        public string Color { get; private set; }
        public decimal Cost { get; private set; }
        public decimal SellingPrice { get; private set; }

        // MongoDB will map this field
        [BsonElement("sizes")]
        private List<ProductSize> _sizes = new();

        // Exposed as read-only (not persisted)
        [BsonIgnore]
        public IReadOnlyCollection<ProductSize> Sizes => _sizes.AsReadOnly();

        // Computed property (not persisted)
        [BsonIgnore]
        public int TotalQuantity => _sizes.Sum(s => s.Quantity);

        // MongoDB needs this
        protected Product() : base() { }

        public Product(string name, string code, string color, decimal cost, decimal sellingPrice, string userId)
            : base(userId)
        {
            Id = Guid.NewGuid().ToString();
            Name = name;
            Color = color;
            Cost = cost;
            SellingPrice = sellingPrice;
            Code = code;
        }

        public void AddSizes(List<ProductSize> productSizes)
        {
            foreach (var item in productSizes)
            {
                _sizes.Add(new ProductSize(item.Size, item.Quantity));
            }
        }

        public void DeductStock(string size, int quantity)
        {
            var existingSize = _sizes.FirstOrDefault(s => s.Size == size);

            if (existingSize == null)
                throw new InvalidOperationException("Size not found");

            existingSize.RemoveStock(quantity);
        }

        public void RemoveSize(string size)
        {
            var sizeToRemove = _sizes.FirstOrDefault(s => s.Size == size);

            if (sizeToRemove == null)
                throw new InvalidOperationException("Size not found");

            _sizes.Remove(sizeToRemove);
        }

        public void UpdateDetails(string name,string code, string color, decimal cost, decimal sellingPrice)
        {
            Name = name;
            Color = color;
            Cost = cost;
            SellingPrice = sellingPrice;
            Code = code;
        }

        public void UpdateSizes(List<ProductSize> sizes)
        {
            foreach (var item in sizes)
            {
                var existingSize = _sizes.FirstOrDefault(s => s.Size == item.Size);

                if (existingSize != null)
                {
                    existingSize.UpdateStock(item.Quantity);
                }
                else
                {
                    _sizes.Add(new ProductSize(item.Size, item.Quantity));
                }
            }
        }
    }
}