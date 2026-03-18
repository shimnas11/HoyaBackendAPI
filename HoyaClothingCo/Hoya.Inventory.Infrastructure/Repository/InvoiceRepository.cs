using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hoya.Inventory.Application.DTOs;
using Hoya.Inventory.Domain.Configurations;
using Hoya.Inventory.Domain.DTO;
using Hoya.Inventory.Domain.Entities;
using Hoya.Inventory.Domain.Interfaces;
using Hoya.Inventory.Infrastructure.Mongo;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Hoya.Inventory.Infrastructure.Repository
{
    public class InvoiceRepository : IInvoiceRepository
    {
        private readonly IMongoCollection<Product> _products;
        private readonly IMongoCollection<Invoice> _invoices;
        private readonly MongoDbContext _context;
        public InvoiceRepository(MongoDbContext context, IOptions<MongoDbSettings> settings)
        {
            _context = context;
            _products = context.Database
                .GetCollection<Product>(settings.Value.ProductCollection);

            _invoices = context.Database
              .GetCollection<Invoice>(settings.Value.InvoiceCollection);
        }

        public async Task AddAsync(Invoice invoice)
        {
            using var session = await _context.Client.StartSessionAsync();
            session.StartTransaction();

            try
            {
                var productIds = invoice.Products.Select(p => p.ProductId).ToList();

                var products = await _products
                    .Find(p => productIds.Contains(p.Id))
                    .ToListAsync();

                foreach (var item in invoice.Products)
                {
                    var product = products.FirstOrDefault(p => p.Id == item.ProductId);

                    if (product == null)
                        throw new Exception($"Product {item.ProductId} not found");

                    var size = product.Sizes.FirstOrDefault(s => s.Size == item.Size);

                    if (size == null || size.Quantity < item.Quantity)
                    {
                        throw new Exception($"Insufficient stock for {product.Name} size {item.Size}");
                    }

                    var filter = Builders<Product>.Filter.And(
                        Builders<Product>.Filter.Eq(p => p.Id, item.ProductId),
                        Builders<Product>.Filter.Eq("sizes.Size", item.Size)
                    );

                    var update = Builders<Product>.Update
                        .Inc("sizes.$.Quantity", -item.Quantity);

                    await _products.UpdateOneAsync(session, filter, update);
                }


                await _invoices.InsertOneAsync(session, invoice);

                await session.CommitTransactionAsync();
            }
            catch
            {
                await session.AbortTransactionAsync();
                throw;
            }
        }

       
        public async Task<List<InvoiceDto>> GetAllAsync()
        {
            var invoices = await _invoices
                .Find(x => x.IsActive && !x.IsDeleted)
                .ToListAsync();

            var productIds = invoices
                .SelectMany(i => i.Products)
                .Select(p => p.ProductId)
                .Distinct()
                .ToList();

            var products = await _products
                .Find(p => productIds.Contains(p.Id))
                .ToListAsync();

            var result = invoices.Select(invoice => new InvoiceDto
            {
                InvoiceNumber = invoice.ReferenceId,
                TotalAmount=invoice.TotalAmount,
                DiscountAmount = invoice.Discount,
                PaymentMode = invoice.PaymentMode,
                ExhibitionId=invoice.ExhibitionId,
                InvoiceDate =invoice.CreatedAt,
                Items = invoice.Products.Select(p =>
                {
                    var product = products.FirstOrDefault(x => x.Id == p.ProductId);

                    return new InvoiceItemDto
                    {
                        ProductId = p.ProductId,
                        ProductName = product.Name,
                        Price = product.SellingPrice,
                        Size = p.Size,
                        Color=product.Color,
                        Quantity = p.Quantity
                    };
                }).ToList()
            }).ToList();

            return result;
        }
        public Task<Invoice> GetInvoiceByIdAsync(string id)
        {
            throw new NotImplementedException();
        }
    }
}
