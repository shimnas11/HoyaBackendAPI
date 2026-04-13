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
                .GetCollection<Product>("Products");

            _invoices = context.Database
              .GetCollection<Invoice>("Invoices");
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



            return await GetInvoiceDetails(invoices);
        }

        public async Task<List<InvoiceDto>> GetAllAsync(string exhitbitionId)
        {
            var invoices = await _invoices
                .Find(x => x.IsActive && !x.IsDeleted && x.ExhibitionId == exhitbitionId)
                .ToListAsync();
            return await GetInvoiceDetails(invoices);
        }

        public Task<Invoice> GetInvoiceByIdAsync(string id)
        {
            throw new NotImplementedException();
        }


        public async Task ReturnItemsAsync(string invoiceId, ReturnOrderDTO returnOrder)
        {
            using var session = await _context.Client.StartSessionAsync();
            session.StartTransaction();

            try
            {
                // 1. Get invoice
                var invoice = await _invoices
                    .Find(i => i.Id == invoiceId)
                    .FirstOrDefaultAsync();

                if (invoice == null)
                    throw new Exception("Invoice not found");

                // 2. Get product
                var product = await _products
                    .Find(p => p.Id == returnOrder.ProductId)
                    .FirstOrDefaultAsync();

                if (product == null)
                    throw new Exception($"Product {returnOrder.ProductId} not found");

                // 3. Validate size
                var size = product.Sizes
                    .FirstOrDefault(s => s.Size == returnOrder.ProductSize);

                if (size == null)
                    throw new Exception($"Size {returnOrder.ProductSize} not found in product {product.Name}");

                // 4. Validate invoice item
                var invoiceItem = invoice.Products
                    .FirstOrDefault(p => p.ProductId == returnOrder.ProductId && p.Size == returnOrder.ProductSize);

                if (invoiceItem == null)
                    throw new Exception("Item not found in invoice");

                if (returnOrder.Quantity <= 0)
                    throw new Exception("Invalid return quantity");

                if (returnOrder.Quantity > invoiceItem.Quantity)
                    throw new Exception("Return quantity exceeds sold quantity");

                // 🔥 Calculate return amount
                decimal returnAmount = invoiceItem.Amount * returnOrder.Quantity;

                // 5. Update stock (ADD back)
                var productFilter = Builders<Product>.Filter.And(
                    Builders<Product>.Filter.Eq(p => p.Id, returnOrder.ProductId),
                    Builders<Product>.Filter.Eq("sizes.Size", returnOrder.ProductSize)
                );

                var productUpdate = Builders<Product>.Update
                    .Inc("sizes.$.Quantity", returnOrder.Quantity);

                await _products.UpdateOneAsync(session, productFilter, productUpdate);

                // 6. Update invoice (REMOVE or REDUCE)
                if (returnOrder.Quantity == invoiceItem.Quantity)
                {
                    // FULL RETURN → REMOVE item
                    await _invoices.UpdateOneAsync(
                        session,
                        Builders<Invoice>.Filter.Eq(i => i.Id, invoiceId),
                        Builders<Invoice>.Update.PullFilter(
                            i => i.Products,
                            p => p.ProductId == returnOrder.ProductId && p.Size == returnOrder.ProductSize
                        )
                    );
                }
                else
                {
                    // PARTIAL RETURN → REDUCE quantity
                    await _invoices.UpdateOneAsync(
                        session,
                        Builders<Invoice>.Filter.And(
                            Builders<Invoice>.Filter.Eq(i => i.Id, invoiceId),
                            Builders<Invoice>.Filter.ElemMatch(i => i.Products,
                                p => p.ProductId == returnOrder.ProductId && p.Size == returnOrder.ProductSize)
                        ),
                        Builders<Invoice>.Update.Inc("Products.$.Quantity", -returnOrder.Quantity)
                    );
                }

                // 7. Update financials
                var financialUpdate = Builders<Invoice>.Update
                    .Inc(i => i.TotalAmount, -returnAmount)
                    .Inc(i => i.NetAmount, -returnAmount);

                await _invoices.UpdateOneAsync(
                    session,
                    Builders<Invoice>.Filter.Eq(i => i.Id, invoiceId),
                    financialUpdate
                );

              
                // 9. Update status
                var updatedInvoice = await _invoices
                    .Find(i => i.Id == invoiceId)
                    .FirstOrDefaultAsync();

                string status = (updatedInvoice.Products == null || !updatedInvoice.Products.Any())
                    ? "FullyReturned"
                    : "PartiallyReturned";

                await _invoices.UpdateOneAsync(
                    session,
                    Builders<Invoice>.Filter.Eq(i => i.Id, invoiceId),
                    Builders<Invoice>.Update.Set(i => i.Status, status)
                );

                await session.CommitTransactionAsync();
            }
            catch
            {
                await session.AbortTransactionAsync();
                throw;
            }
        }
        private async Task<List<InvoiceDto>> GetInvoiceDetails(List<Invoice> invoices)
        {
            var productIds = invoices
              .SelectMany(i => i.Products)
              .Select(p => p.ProductId)
              .Distinct()
              .ToList();

            var products = await _products
                .Find(p => productIds.Contains(p.Id))
                .ToListAsync();

            var result= invoices.Select(invoice => new InvoiceDto
            {
                InvoiceNumber = invoice.ReferenceId,
                Id = invoice.Id,
                TotalAmount = invoice.TotalAmount,
                DiscountAmount = invoice.Discount,
                PaymentMode = invoice.PaymentMode,
                ExhibitionId = invoice.ExhibitionId,
                InvoiceDate = invoice.CreatedAt,
                Status = invoice.Status,
                Items = invoice.Products.Select(p =>
                {
                    var product = products.FirstOrDefault(x => x.Id == p.ProductId);

                    return new InvoiceItemDto
                    {
                        ProductId = p.ProductId,
                        ProductName = product.Name,
                        Price = product.SellingPrice,
                        Size = p.Size,
                        Color = product.Color,
                        Quantity = p.Quantity
                    };
                }).ToList()
            }).ToList();
            
            return result;

        }
    }
}
