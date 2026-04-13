using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;
using Hoya.Inventory.Domain.Configurations;
using Hoya.Inventory.Domain.DTO;
using Hoya.Inventory.Domain.Entities;
using Hoya.Inventory.Domain.Entities.Exhibition;
using Hoya.Inventory.Domain.Interfaces;
using Hoya.Inventory.Infrastructure.Mongo;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Hoya.Inventory.Infrastructure.Repository
{
    public class ExhibitionRepository : IExhibitionRepository
    {

        private readonly IMongoCollection<Exhibition> _exhibitions;
        private readonly IMongoCollection<Invoice> _invoices;
        private readonly IMongoCollection<Product> _products;
        private readonly MongoDbContext _context;
        public ExhibitionRepository(MongoDbContext context, IOptions<MongoDbSettings> settings)
        {
            _context = context;
            _exhibitions = context.Database
                .GetCollection<Exhibition>("Exhibitions");

            _invoices = context.Database
              .GetCollection<Invoice>("Invoices");

            _products = context.Database
              .GetCollection<Product>("Products");

        }
        public async Task AddAsync(Exhibition exhibition)
        {
            using var session = await _context.Client.StartSessionAsync();
            session.StartTransaction();

            try
            {

                await _exhibitions.InsertOneAsync(session, exhibition);

                await session.CommitTransactionAsync();
            }
            catch
            {
                await session.AbortTransactionAsync();
                throw;
            }
        }

        public async Task<List<Exhibition>> GetAll()
        {
            return await _exhibitions.Aggregate()
                .Match(x => x.IsActive && !x.IsDeleted)
                .ToListAsync();
        }

        public async Task<Exhibition> GetByIdAsync(string id)
        {
            var result = await _exhibitions.Find(x => x.Id == id).FirstOrDefaultAsync();

            return result;

        }

        public async Task<ExhibitionDTO> GetDetails(string id)
        {
            var exhibition = await _exhibitions
             .Find(x => x.Id == id)
             .FirstOrDefaultAsync();
            if (exhibition != null)
            {
                var invoices = _invoices.Aggregate()
                    .Match(i => i.ExhibitionId == exhibition.Id)
                    .ToList();

                var exhibitionDetails = new ExhibitionDTO
                {
                    Id = exhibition.Id,
                    Name = exhibition.Name,
                    Place = exhibition.Place,
                    StartDate = exhibition.StartDate,
                    EndDate = exhibition.EndDate,
                    BookingCost = exhibition.BookingCost,
                    TotalExpense = exhibition.TotalExpense,
                    Expenses = exhibition.Expenses?.Select(e => new ExhibitionExpenseDTO
                    {
                        Id = e.Id,
                        ExhibitionId = e.ExhibitionId,
                        Name = e.Name,
                        Cost = e.Cost,
                        CreatedDate = e.CreatedDate
                    }).ToList(),

                };
                exhibitionDetails.Invoices = invoices.Select(i => new InvoiceDto
                {
                    Id = i.Id,
                    ExhibitionId = i.ExhibitionId,
                    TotalAmount = i.TotalAmount,
                    DiscountAmount = i.Discount,
                    InvoiceDate = i.CreatedAt,
                    InvoiceNumber = i.ReferenceId,
                    PaymentMode = i.PaymentMode,
                }).ToList();

                return exhibitionDetails;
            }
            return null;
        }

        public async Task<ExhibitionOverviewDTO> GetOverview(string id)
        {
            var overview = new ExhibitionOverviewDTO();
            var exhibition = await _exhibitions
             .Find(x => x.Id == id)
             .FirstOrDefaultAsync();
            if (exhibition != null)
            {
                var invoices = _invoices.Aggregate()
                    .Match(i => i.ExhibitionId == exhibition.Id)
                    .ToList();


                var productids = invoices
                                .SelectMany(i => i.Products)
                                .Select(p => p.ProductId)
                                .Distinct()
                                .ToList();
                var productDetails = await _products.Aggregate().Match(x => productids
                                    .Any(c => c == x.Id)).ToListAsync();
               

                foreach (var invoice in invoices)
                {
                    overview.NetAmount += invoice.NetAmount;
                    overview.TotalDiscount += invoice.Discount;
                    overview.TotalSales += invoice.TotalAmount;
                    decimal itemPurchasedSum = 0;

                    foreach (var product in invoice.Products)
                    {
                        var pr= productDetails.Find(x=>x.Id==product.ProductId);
                        if (pr != null)
                        {
                            itemPurchasedSum += pr.Cost;
                        }
                    }
                    overview.Profit +=( invoice.TotalAmount - itemPurchasedSum);
                }



            }
            return overview;
        }

        public async Task UpdateAsync(Exhibition exhibition)
        {
            using var session = await _context.Client.StartSessionAsync();
            session.StartTransaction();

            try
            {

                var filter = Builders<Exhibition>.Filter.Eq(x => x.Id, exhibition.Id);

                await _exhibitions.ReplaceOneAsync(filter, exhibition);

                await session.CommitTransactionAsync();
            }
            catch
            {
                await session.AbortTransactionAsync();
                throw;
            }
        }
    }
}
