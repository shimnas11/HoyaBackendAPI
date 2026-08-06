
using System.Linq;
using Hoya.Inventory.Domain.DTO;
using Hoya.Inventory.Domain.Entities;
using Hoya.Inventory.Domain.Entities.Exhibition;
using Hoya.Inventory.Domain.Entities.Misc;
using Hoya.Inventory.Domain.Interfaces;
using Hoya.Inventory.Infrastructure.Mongo;
using MongoDB.Driver;

namespace Hoya.Inventory.Infrastructure.Repository
{
    public class DashboardRepository : IDashboardRepository
    {
        private readonly IProductRepository _productRepo;
        private readonly IInvoiceRepository _invoiceRepo;
        private readonly IExhibitionRepository _exhibitionRepo;
        private readonly IDamagedProductRepository _damageRepo;
        private readonly IMongoCollection<Invoice> _invoices;
        private readonly IMongoCollection<Product> _products;
        private readonly IMongoCollection<DamagedProduct> _damageProducts;
        private readonly MongoDbContext _context;
        public DashboardRepository(MongoDbContext context, IDamagedProductRepository damageRepo,IProductRepository productRepo, IInvoiceRepository invoiceRepo, IExhibitionRepository exhibitionRepo)
        {
            _productRepo = productRepo;
            _invoiceRepo = invoiceRepo;
            _exhibitionRepo = exhibitionRepo;
            _damageRepo = damageRepo;
            _invoices = context.Database
              .GetCollection<Invoice>("Invoices");

            _products = context.Database
              .GetCollection<Product>("Products");
            _damageProducts = context.Database
              .GetCollection<DamagedProduct>("DamagedProducts");
        }

        public async Task<DashboardDTO> GetDashboardOverviewAsync()
        {
            var products = await _productRepo.GetAllAsync();
            var invoices = await _invoiceRepo.GetAllAsync();
            var exhibitions = await _exhibitionRepo.GetAll();
            var damageProducts = await _damageRepo.GetAllAsync();
            var response = new DashboardDTO()
            {
                TotalProducts = 0,
                TotalSold = 0,
                Profit = 0,
                Revenue = 0,
                TotalExpense = 0,
                DamageCount=0,
                TotalDamage = 0
            };
            response.TotalDamage = damageProducts.Sum(damage => damage.Quantity * damage.BuyingPrice);
            response.DamageCount = damageProducts.Sum(damage => damage.Quantity );
            products.ForEach(product =>
                   {
                       response.TotalProducts += product.Sizes.Sum(size => size.Quantity);

                   });

            invoices.ForEach(invoice =>
            {
                response.TotalSold += invoice.Items.Sum(item => item.Quantity);
                response.Revenue += invoice.TotalAmount;
                response.Profit += invoice.Items.Sum(item =>
                {
                    var product = products.FirstOrDefault(p => p.Id == item.ProductId);
                    if (product != null)
                    {
                        return (item.Price - product.Cost) * item.Quantity;
                    }
                    return 0;
                });
                response.Profit -= invoice.DiscountAmount;
            });

            exhibitions.ForEach(exhibition =>
            {
                response.Profit -= exhibition.NetAmount;
                response.TotalExpense += exhibition.NetAmount;
            });
            return response;

        }

        public async Task<List<HotSellingProductDTO>> GetHotSellingProductsAsync()
        {
            var invoices = await _invoiceRepo.GetAllAsync();
            var topRevenueProducts = invoices
     .SelectMany(x => x.Items)
     .GroupBy(x => x.ProductId)
     .Select(g => new HotSellingProductDTO
     {
         ProductName = g.First().ProductName,
         Revenue = g.Sum(x => x.Quantity * x.Price)
     })
     .OrderByDescending(x => x.Revenue)
     .Take(10)
     .ToList();
        return topRevenueProducts;
        }
        public async Task<List<ExhibitionOverviewDTO>> GetExhibitionOverviewAsync()
        {

            var result= new List<ExhibitionOverviewDTO>();
            var exhibitions = await _exhibitionRepo.GetAll();
            if (exhibitions != null)
            {
                var invoices = _invoices.Aggregate()
                   .Match(i => exhibitions.Any(c=>c.Id== i.ExhibitionId))
                   .ToList();


                var productids = invoices
                            .SelectMany(i => i.Products)
                            .Select(p => p.ProductId)
                            .Distinct()
                            .ToList();
                var productDetails = await _products.Aggregate().Match(x => productids
                                    .Any(c => c == x.Id)).ToListAsync();


                foreach (var exhibition in exhibitions)
                {
                    var overview = new ExhibitionOverviewDTO();
                    overview.Name = exhibition.Name;
                    overview.TotalItemSelled = 0;
                    overview.ExhibitionId= exhibition.Id;
                    foreach (var invoice in invoices.Where(x=>x.ExhibitionId==exhibition.Id).ToList())
                    {
                        overview.NetAmount += invoice.NetAmount;
                        if (invoice.Discount > 0)
                        {
                            overview.TotalDiscount += invoice.Discount;
                        }
                        else
                        {
                            overview.Additional += invoice.Discount;
                        }
                        overview.TotalSales += invoice.TotalAmount;
                        decimal itemPurchasedSum = 0;

                        foreach (var product in invoice.Products)
                        {
                            var pr = productDetails.Find(x => x.Id == product.ProductId);
                            if (pr != null)
                            {
                                overview.TotalItemSelled++;
                                itemPurchasedSum += pr.Cost;
                            }
                        }
                        overview.Profit += (invoice.NetAmount - itemPurchasedSum);
                    }
                    overview.Profit = overview.Profit - (exhibition.BookingCost + exhibition.TotalExpense);
                    result.Add(overview);
                }
                
            }
            return result.OrderByDescending(x=>x.Profit).Take(10).ToList();
        }
    }
}
